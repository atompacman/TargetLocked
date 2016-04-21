using UnityEngine;

namespace TargetLocked
{
    public sealed class ExplosionGrenade : AbstractGrenade
    {
        public static readonly new GameObject PREFAB;

        static ExplosionGrenade()
        {
            PREFAB = Resources.Load<GameObject>("Prefabs/Grenade/ExplosionGrenade");
        }

        protected override void AddExplosionForce(Rigidbody i_RigidBody)
        {
            i_RigidBody.AddExplosionForce(10, transform.position, 100, 10, ForceMode.Impulse);
        }
    }
}