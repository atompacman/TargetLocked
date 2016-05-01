using UnityEngine;

namespace TargetLocked.Weapons.EXIMG
{
    public abstract class FireChargeWeapon : Weapon
    {
        #region Compile-time constants

        private const float FIRE_CHARGE_RATE = 1f;
        private const float COOLDOWN_TIME = 0.2f;

        #endregion

        #region Fields

        public float FireCharge;
        public float LastFireCharge;

        #endregion

        #region Methods

        protected override void Start()
        {
            FireCharge = 0;
            LastFireCharge = 0;
            base.Start();
        }

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
        }

        protected abstract void Fire();

        protected override void OnTriggerReleased()
        {
            // Don't fire during cooldown
            IsFiring = Time.fixedTime - LastFireTime > COOLDOWN_TIME;
            if (IsFiring)
            {
                // Update last fire time
                LastFireTime = Time.fixedTime;

                LastFireCharge = FireCharge;

                // Fire gun
                Fire();
            }

            // Reset charge
            FireCharge = 0;
        }

        #endregion
    }
}