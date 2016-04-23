using UnityEngine;

namespace TargetLocked.Weapons.EXIMG
{
    [WeaponInfo("EXIM-G", "Explosion", "Implosion")]
    // ReSharper disable once InconsistentNaming
    public sealed class EXIMG : Weapon
    {
        #region Compile-time constants

        private const float FIRE_CHARGE_RATE = 1f;
        private const float MAX_FORCE = 4000;
        private const float MIN_FORCE = 150;

        #endregion

        #region Fields

        private float m_FireCharge;

        #endregion

        #region Methods

        protected override void Init()
        {}

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

        protected override void OnTriggerPulled()
        {
            m_FireCharge = 0;
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
            var cam = Common.PlayerObj.GetComponentInChildren<Camera>();
            grenade.transform.position = transform.position + Vector3.up * Common.EYES_HEIGHT +
                                         cam.transform.forward;

            // Shoot grenade
            var rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = Common.PlayerObj.GetComponent<Rigidbody>().velocity;
            var force = MIN_FORCE + m_FireCharge * (MAX_FORCE - MIN_FORCE);
            rb.AddForce(cam.transform.forward * force, ForceMode.Force);

            // Reset charge
            m_FireCharge = 0;
        }

        #endregion
    }
}