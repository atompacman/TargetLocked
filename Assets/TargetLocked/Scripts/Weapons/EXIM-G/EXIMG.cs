using UnityEngine;

namespace TargetLocked.Weapons.EXIMG
{
    // ReSharper disable once InconsistentNaming
    public sealed class EXIMG : Weapon
    {
        #region Compile-time constants

        public new const string ASSET_DIR = Weapon.ASSET_DIR + "EXIM-G/";

        private const float FIRE_CHARGE_RATE = 1f;
        private const float MAX_FORCE = 2000;

        #endregion Compile-time constants

        #region Fields

        private float m_FireCharge;

        #endregion Fields

        #region Methods

        protected override void OnTriggerPulled()
        {
            m_FireCharge = 0;
        }

        protected override void OnTriggerHeld()
        {
            // Charge fire
            m_FireCharge += Time.deltaTime * FIRE_CHARGE_RATE;

            // If charge exceeds maximum, force shoot
            if (m_FireCharge > 1)
            {
                OnTriggerReleased();
            }
        }

        protected override void OnTriggerReleased()
        {
            // Create grenade
            var grenade = Instantiate(Grenade.PREFAB);

            // Set correct grenade type according to gun mode
            grenade.AddComponent(GunMode == Mode.MAIN
                ? typeof(ExplosionGrenade)
                : typeof(ImplosionGrenade));

            // Set initial grenade position in front of player
            var cam = GetComponentInChildren<Camera>();
            grenade.transform.position = transform.position + cam.transform.forward;

            // Shoot grenade
            var rb = grenade.GetComponent<Rigidbody>();
            var force = m_FireCharge * MAX_FORCE;
            rb.AddExplosionForce(force, transform.position, 1, 0, ForceMode.Force);
            rb.AddForce(Common.PlayerObj.GetComponent<Rigidbody>().velocity);

            // Reset charge
            m_FireCharge = 0;
        }

        #endregion Methods
    }
}