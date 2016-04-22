using JetBrains.Annotations;
using TargetLocked.Weapons.Eximg;
using UnityEngine;

namespace TargetLocked.Weapons
{
    public sealed class EquipementManager : MonoBehaviour
    {
        #region Fields

        private Weapon m_MainWeapon;

        #endregion Fields

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            m_MainWeapon = gameObject.AddComponent<EXIMG>();
            m_MainWeapon.transform.parent = transform;
        }
        
        [UsedImplicitly]
        private void Update()
        {
            // TODO: switch weapon
        }

        #endregion Methods
    }
}