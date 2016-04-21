using UnityEngine;

namespace TargetLocked
{
    public sealed class ImplosionGrenade : AbstractGrenade
    {
        public static readonly new GameObject PREFAB;

        static ImplosionGrenade()
        {
            PREFAB = Resources.Load<GameObject>("Prefabs/Grenade/ImplosionGrenade");
        }

        protected override void AddExplosionForce(Rigidbody i_RigidBody)
        {
            Vector3 explPos = 2 * i_RigidBody.transform.position - transform.position;
            i_RigidBody.AddExplosionForce(10, explPos, 100, 10, ForceMode.VelocityChange);
        }
    }
}