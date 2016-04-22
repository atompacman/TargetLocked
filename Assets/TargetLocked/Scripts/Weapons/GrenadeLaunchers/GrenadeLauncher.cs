using System;
using UnityEngine;

namespace TargetLocked.Weapons.GrenadeLaunchers
{
    public abstract class GrenadeLauncher : Weapon
    {
        #region Compile-time constants

        public new const string ASSET_DIR = Weapon.ASSET_DIR + "GrenadeLaunchers/";

        private const float FIRE_FORCE = 1000;

        #endregion

        protected Type GrenadeType;

        #region Methods

        public override void Fire()
        {
            // Create grenade
            var grenade = Instantiate(Grenade.PREFAB);
            grenade.AddComponent(GrenadeType);

            // Set initial grenade position in front of player
            var cam = GetComponentInChildren<Camera>();
            grenade.transform.position = transform.position + cam.transform.forward;

            // Shoot grenade
            var rb = grenade.GetComponent<Rigidbody>();
            rb.AddExplosionForce(FIRE_FORCE, transform.position, 1, 0, ForceMode.Force);
        }

        #endregion
    }
}