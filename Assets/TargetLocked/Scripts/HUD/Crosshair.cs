using JetBrains.Annotations;
using TargetLocked.Weapons;
using TargetLocked.Weapons.EXIMG;
using UnityEngine;
using UnityEngine.UI;

namespace TargetLocked.HUD
{
    public sealed class Crosshair : MonoBehaviour
    {
        #region Methods

        [UsedImplicitly]
        private void Update()
        {
            var em = Common.PlayerObj.GetComponent<EquipementManager>();
            var fcw = em.MainWeapon as EXIMG;
            if (fcw != null)
            {
                GetComponent<Image>().color = fcw.GrenadeColor;
            }
        }

        #endregion
    }
}