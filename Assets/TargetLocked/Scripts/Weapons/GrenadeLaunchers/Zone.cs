using JetBrains.Annotations;
using UnityEngine;

namespace TargetLocked.Weapons.GrenadeLaunchers
{
    public sealed class Zone : MonoBehaviour
    {
        #region Compile-time constants

        private const float DURATION = 2;
        private const string MAT_COLOR_PROP_NAME = "_TintColor";
        private const string ZONE_MAT_NAME = "Zone";

        #endregion

        #region Runtime constants

        private static readonly Material ZONE_MATERIAL;

        #endregion

        #region Fields

        private float m_CreationTime;

        #endregion

        #region Constructors

        static Zone()
        {
            ZONE_MATERIAL = Resources.Load<Material>(GrenadeLauncher.ASSET_DIR + ZONE_MAT_NAME);
        }

        #endregion

        #region Methods

        public static void CreateExplosionZone(Vector3 i_Pos, Color i_Color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = i_Pos;
            go.transform.localScale = Vector3.one * Grenade.EXPLOSION_RADIUS * 2;
            go.AddComponent<Zone>();
            go.GetComponent<SphereCollider>().enabled = false;
            go.GetComponent<MeshRenderer>().material = ZONE_MATERIAL;
            go.GetComponent<MeshRenderer>().material.SetColor(MAT_COLOR_PROP_NAME, i_Color);
        }

        [UsedImplicitly]
        private void Start()
        {
            m_CreationTime = Time.fixedTime;
        }

        [UsedImplicitly]
        private void Update()
        {
            // Zone alpha decrease with time
            var mat = GetComponent<MeshRenderer>().material;
            var color = mat.GetColor(MAT_COLOR_PROP_NAME);
            color.a = 1 - Mathf.Pow((Time.fixedTime - m_CreationTime) / DURATION, 0.1f);
            mat.SetColor(MAT_COLOR_PROP_NAME, color);

            // Remove gameobject after a certain time
            if (Time.fixedTime - m_CreationTime > DURATION)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}