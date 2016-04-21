using UnityEngine;

namespace TargetLocked
{
    public abstract class AbstractGrenade : MonoBehaviour
    {
        public static readonly GameObject PREFAB;

        private const float MAX_TIME_BEFORE_EXPLODE = 2;

        private float m_CreationTime;

        static AbstractGrenade()
        {
            PREFAB = Resources.Load<GameObject>("Prefabs/Grenade/Prefab");
        }

        private void Start()
        {
            m_CreationTime = Time.fixedTime;
        }

        private void Update()
        {
            if (Time.fixedTime - m_CreationTime > MAX_TIME_BEFORE_EXPLODE)
            {
                Explode();
            }
        }

        private void OnCollisionsEnter(Collider i_Other)
        {
            Debug.Log("Boink");
        }

        private void Explode()
        {
            var go = GameObject.Find("Cubes");
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                AddExplosionForce(go.transform.GetChild(i).GetComponent<Rigidbody>());
            }
            Destroy(gameObject);
        }

        protected abstract void AddExplosionForce(Rigidbody i_RigidBody);
    }
}