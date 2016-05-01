using UnityEngine;

namespace TargetLocked.Weapons.EXIMG
{
    [WeaponInfo("EXIM-G", "Explosion", "Implosion")]
    // ReSharper disable once InconsistentNaming
    public sealed class EXIMG : FireChargeWeapon
    {
        #region Compile-time constants

        private const float MAX_FORCE = 4000;
        private const float MIN_FORCE = 150;

        #endregion

        #region Fields

        #endregion

        #region Methods

        protected override void Fire()
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
            var force = MIN_FORCE + FireCharge * (MAX_FORCE - MIN_FORCE);
            rb.AddForce(cam.transform.forward * force, ForceMode.Force);
        }

        #endregion
    }
}