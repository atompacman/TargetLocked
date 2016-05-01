Shader "Unlit/FireCharge"
{
    Properties 
    {
        _Charge    ("Charge",    Range(0,1)) = 0.5
        _Radius    ("Radius",    Range(0,1)) = 0.5
        _Thickness ("Thickness", Range(0,1)) = 0.5
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

            fixed _Charge;
            fixed _Radius;
            fixed _Thickness;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Centered UV coordinates
                fixed2 centered = i.uv - 0.5;

                // Return transparent if outside of the ring
                fixed dist2CenterSqrd = centered.x * centered.x + centered.y * centered.y;
                fixed maxRad = 0.25 * _Radius * _Radius;
                fixed minRad = 0.25 * (1 - _Thickness) * _Radius * _Radius;
                if (dist2CenterSqrd > maxRad || dist2CenterSqrd < minRad)
                {
                    return 0;
                }

                // Return transparent depending on charge
                fixed angle = atan2(centered.y, centered.x) / (2 * PI) + 0.5;
                if (angle <= (1 - _Charge))
                {
                    return 0;
                }

                return fixed4(1, 1, 1, pow(_Charge, 0.8) * 0.5);
            }
            ENDCG
        }
    }
}