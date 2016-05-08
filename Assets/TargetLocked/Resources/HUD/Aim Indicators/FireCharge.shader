Shader "Unlit/FireCharge"
{
    Properties 
    {
        _Charge    ("Charge",    Range(0,1)) = 0.5
        _Radius    ("Radius",    Range(0,1)) = 0.5
        _Thickness ("Thickness", Range(0,1)) = 0.5
        _Color     ("Color",     Color)      = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define PI 3.14159265358979323846
            #define MIN_ALPHA 0.15
            #define MAX_ALPHA 0.7
            #define ALPHA_CURVE 0.8

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv     : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Charge;
            float _Radius;
            float _Thickness;
            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // Centered UV coordinates
                float2 centered = i.uv - 0.5;

                // Adapt alpha according to charge
                float alpha = pow(_Charge, ALPHA_CURVE) * (MAX_ALPHA - MIN_ALPHA) + MIN_ALPHA;

                // Fade alpha outside min and max radius
                float rad = sqrt(centered.x * centered.x + centered.y * centered.y);
                float maxRad = 0.5 * _Radius;
                float minRad = 0.5 * _Radius * (1 - _Thickness);
                if (rad > maxRad)
                {
                    alpha *= exp(-(rad - maxRad) * 100);
                }
                if (rad < minRad)
                {
                    alpha *= exp(-(minRad - rad) * 100);
                }

                // Charging animation
                float angle = atan2(centered.y, centered.x) / (2 * PI) + 0.5;
                if (angle <= (1 - _Charge))
                {
                    alpha = 0;
                }

                return float4(_Color.rgb, alpha);
            }
            ENDCG
        }
    }
}