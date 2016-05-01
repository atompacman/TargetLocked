using JetBrains.Annotations;
using TargetLocked.Weapons;
using TargetLocked.Weapons.EXIMG;
using UnityEngine;
using UnityEngine.UI;

namespace TargetLocked.HUB
{
    public sealed class FireChargeIndicator : MonoBehaviour
    {
        #region Compile-time constants

        private const string CHARGE_SHADER_PROP_NAME = "_Charge";

        private const float MIN_CHARGE = 0.03f;

        #endregion Compile-time constants

        #region Fields

        private Material m_Material;

        #endregion Fields

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            m_Material = GetComponent<Image>().material;
        }

        [UsedImplicitly]
        private void Update()
        {
            // Get main weapon
            var em = Common.PlayerObj.GetComponent<EquipementManager>();
            var fcw = em.MainWeapon as FireChargeWeapon;

            // If it's a chargeable weapon, update charge progression
            // ReSharper disable once MergeConditionalExpression
            var charge = fcw == null ? 0 : fcw.FireCharge ;
            if (charge != 0)
            {
                charge += MIN_CHARGE;
            }
            m_Material.SetFloat(CHARGE_SHADER_PROP_NAME, charge);
        }

        #endregion Methods
    }
}