using UnityEngine;

namespace TargetLocked.Weapons.Eximg
{
    public sealed class ExplosionGrenade : Grenade
    {
        #region Compile-time constants

        private const float EXPLOSION_UPWARD_MODIFICATOR = 10;

        #endregion Compile-time constants

        #region Runtime constants

        private static readonly Color COLOR = Color.red;

        #endregion Runtime constants

        #region Methods

        protected override void AddExplosionForce(Rigidbody i_RigidBody)
        {
            i_RigidBody.AddExplosionForce(EXPLOSION_FORCE, transform.position,
                EXPLOSION_RADIUS, EXPLOSION_UPWARD_MODIFICATOR, EXPLOSION_MODE);
        }

        protected override Color GetColor()
        {
            return COLOR;
        }

        #endregion Methods
    }
}