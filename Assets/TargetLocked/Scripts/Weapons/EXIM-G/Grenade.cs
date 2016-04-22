using JetBrains.Annotations;
using UnityEngine;

namespace TargetLocked.Weapons.Eximg
{
    public abstract class Grenade : MonoBehaviour
    {
        #region Compile-time constants

        public const float EXPLOSION_RADIUS = 20;
        protected const float EXPLOSION_FORCE = 6000;
        protected const ForceMode EXPLOSION_MODE = ForceMode.Force;
        private const float DETONATION_TIME = 2;

        private const string MAT_COLOR_PROP_NAME = "_TintColor";
        private const string PREFAB_NAME = "Grenade";

        #endregion Compile-time constants

        #region Runtime constants

        public static readonly GameObject PREFAB;

        #endregion Runtime constants

        #region Fields

        private float m_CreationTime;

        #endregion Fields

        #region Constructors

        static Grenade()
        {
            PREFAB = Resources.Load<GameObject>(EXIMG.ASSET_DIR + PREFAB_NAME);
        }

        #endregion Constructors

        #region Abstract methods

        protected abstract void AddExplosionForce(Rigidbody i_RigidBody);

        protected abstract Color GetColor();

        #endregion Abstract methods

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            m_CreationTime = Time.fixedTime;

            // Set color
            GetComponent<MeshRenderer>().material.SetColor(MAT_COLOR_PROP_NAME, GetColor());
            GetComponent<TrailRenderer>().material.SetColor(MAT_COLOR_PROP_NAME, GetColor());
        }

        [UsedImplicitly]
        private void Update()
        {
            // Explode after a certain time
            if (Time.fixedTime - m_CreationTime < DETONATION_TIME)
            {
                return;
            }

            // Add explosion forces to every object with physics
            var go = GameObject.Find("Cubes");
            for (var i = 0; i < go.transform.childCount; ++i)
            {
                // Grenade subclass knows how to create the force
                AddExplosionForce(go.transform.GetChild(i).GetComponent<Rigidbody>());
            }

            // Mark explosion zone
            Zone.CreateExplosionZone(transform.position, GetColor());

            // Destroy this game object
            Destroy(gameObject);
        }

        #endregion Methods
    }
}