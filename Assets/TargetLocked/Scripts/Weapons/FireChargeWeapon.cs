using UnityEngine;

namespace TargetLocked.Weapons.EXIMG
{
    public abstract class FireChargeWeapon : Weapon
    {
        #region Compile-time constants

        private const float FIRE_CHARGE_RATE = 1f;

        #endregion

        #region Fields

        public float FireCharge;

        #endregion

        #region Methods

        protected override void OnTriggerHeld()
        {
            // Charge fire
            FireCharge += Time.deltaTime * FIRE_CHARGE_RATE;

            // If charge exceeds maximum, force shoot
            if (FireCharge > 1)
            {
                OnTriggerReleased();
            }
        }

        protected override void OnTriggerPulled()
        {
            FireCharge = 0;
        }

        protected abstract void Fire();

        protected override void OnTriggerReleased()
        {
            // Fire gun
            Fire();

            // Reset charge
            FireCharge = 0;
        }

        #endregion
    }
}