using JetBrains.Annotations;
using TargetLocked.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace TargetLocked.HUD
{
    public sealed class WeaponIndicator : MonoBehaviour
    {
        #region Compile-time constants

        private const string MODE_OBJ_NAME = "Mode";
        private const string WEAPON_OBJ_NAME = "Weapon";

        #endregion Compile-time constants

        #region Fields

        private Text m_ModeName;
        private Text m_WeaponName;

        #endregion Fields

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            m_WeaponName = transform.FindChild(WEAPON_OBJ_NAME).GetComponent<Text>();
            m_ModeName = transform.FindChild(MODE_OBJ_NAME).GetComponent<Text>();
        }

        [UsedImplicitly]
        private void Update()
        {
            var mw = Common.PlayerObj.GetComponent<EquipementManager>().MainWeapon;
            m_WeaponName.text = "Weapon: " + mw.Info.Name;
            m_ModeName.text = "Mode: " +
                              (mw.GunMode == Weapon.Mode.MAIN
                                  ? mw.Info.MainModeName
                                  : mw.Info.AltModeName);
        }

        #endregion Methods
    }
}