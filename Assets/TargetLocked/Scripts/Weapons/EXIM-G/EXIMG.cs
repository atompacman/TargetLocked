using System;
using TargetLocked.HUD;
using UnityEngine;

// ReSharper disable ConvertPropertyToExpressionBody

namespace TargetLocked.Weapons.EXIMG
{
    [WeaponInfo("EXIM-G", "Explosion", "Implosion")]
    // ReSharper disable once InconsistentNaming
    public sealed class EXIMG : FireChargeWeapon
    {
        #region Compile-time constants

        private const float MAX_FORCE = 6000;
        private const float MIN_FORCE = 250;
        private const float AIM_HELPER_FACTOR = 0.5f;
        /** Velocity at which aim helper is fully active */
        private const float VAWAHIFA = VelocityMagnitudeSliderIndicator.MAX_VALUE * 0.5f;

        #endregion

        #region Properties

        public Type GrenadeType
        {
            get
            {
                return GunMode == Mode.MAIN ? typeof(ExplosionGrenade) : typeof(ImplosionGrenade);
            }
        }

        public Color GrenadeColor
        {
            get { return GunMode == Mode.MAIN ? ExplosionGrenade.COLOR : ImplosionGrenade.COLOR; }
        }

        #endregion

        #region Methods

        protected override void Fire()
        {
            // Create grenade
            var grenade = Instantiate(AbstractGrenade.PREFAB);

            // Set correct grenade type according to gun mode
            grenade.AddComponent(GrenadeType);

            // Set initial grenade position in front of player
            var cam = Common.PlayerObj.GetComponentInChildren<Camera>();
            grenade.transform.position = transform.position + Vector3.up * Common.EYES_HEIGHT +
                                         cam.transform.forward;

            // Shoot grenade
            var rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = Common.PlayerObj.GetComponent<Rigidbody>().velocity;
            var force = MIN_FORCE + FireCharge * (MAX_FORCE - MIN_FORCE);
            var aimHelp = Mathf.Clamp01(rb.velocity.magnitude / VAWAHIFA) * AIM_HELPER_FACTOR;
            var dir = Vector3.Lerp(cam.transform.forward, -rb.velocity.normalized, aimHelp);
            rb.AddForce(dir * force, ForceMode.Force);
        }

        #endregion
    }
}