using JetBrains.Annotations;
using TargetLocked.Weapons.GrenadeLaunchers;
using UnityEngine;

namespace TargetLocked.Weapons
{
    public sealed class Arsenal : MonoBehaviour
    {
        #region Compile-time constants

        private const int LEFT_CLICK = 0;
        private const int RIGHT_CLICK = 1;

        #endregion Compile-time constants

        #region Fields

        private Weapon m_LeftWeapon;
        private Weapon m_RightWeapon;

        #endregion Fields

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            m_LeftWeapon = gameObject.AddComponent<Exploder>();
            m_RightWeapon = gameObject.AddComponent<Imploder>();
        }

        [UsedImplicitly]
        private void Update()
        {
            // Check guns are fired
            if (Input.GetMouseButtonDown(LEFT_CLICK))
            {
                m_LeftWeapon.Fire();
            }
            if (Input.GetMouseButtonDown(RIGHT_CLICK))
            {
                m_RightWeapon.Fire();
            }
        }

        #endregion Methods
    }
}