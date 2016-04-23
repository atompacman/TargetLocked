using JetBrains.Annotations;
using UnityEngine;

namespace TargetLocked.Weapons
{
    public sealed class EquipementManager : MonoBehaviour
    {
        #region Fields

        private Weapon m_MainWeapon;

        #endregion

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            m_MainWeapon = gameObject.AddComponent<EXIMG.EXIMG>();
            m_MainWeapon.transform.parent = transform;
        }

        [UsedImplicitly]
        private void Update()
        {
            // TODO: switch weapon
        }

        #endregion
    }
}