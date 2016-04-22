using UnityEngine;

namespace TargetLocked.Weapons.Eximg
{
    public sealed class ImplosionGrenade : Grenade
    {
        #region Compile-time constants

        private const float EXPLOSION_UPWARD_MODIFICATOR = 0;

        #endregion Compile-time constants

        #region Runtime constants

        private static readonly Color COLOR = Color.green;

        #endregion Runtime constants

        #region Methods

        protected override void AddExplosionForce(Rigidbody i_RigidBody)
        {
            // Place explosion behind object to create and implosion effect
            var pos = 2 * i_RigidBody.transform.position - transform.position;
            i_RigidBody.AddExplosionForce(EXPLOSION_FORCE, pos, EXPLOSION_RADIUS,
                EXPLOSION_UPWARD_MODIFICATOR, EXPLOSION_MODE);
        }

        protected override Color GetColor()
        {
            return COLOR;
        }

        #endregion Methods
    }
}