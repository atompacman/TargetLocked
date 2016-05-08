using JetBrains.Annotations;
using TargetLocked.Weapons;
using TargetLocked.Weapons.EXIMG;
using UnityEngine;
using UnityEngine.UI;

namespace TargetLocked.HUD
{
    [RequireComponent(typeof(Image))]
    public sealed class FireChargeIndicator : MonoBehaviour
    {
        #region Compile-time constants

        private const string CHARGE_SHADER_PROP_NAME = "_Charge";
        private const string RADIUS_SHADER_PROP_NAME = "_Radius";
        private const string THICKNESS_SHADER_PROP_NAME = "_Thickness";
        private const string COLOR_SHADER_PROP_NAME = "_Color";

        private const float RADIUS = 0.5f;
        private const float THICKNESS = 0.3f;

        private const float MIN_CHARGE = 0.03f;
        private const float UNCHARGE_RATE = 4;

        private const float FIRING_ANIM_RADIUS_AMPLITUDE = 0.25f;
        private const float FIRING_ANIM_THICNKESS_AMPLITUDE = 0.08f;
        private const float FIRING_ANUM_SPEED = 0.08f;

        #endregion

        #region Fields

        private float m_DisplayedCharge;
        private Material m_Material;

        #endregion

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            m_Material = GetComponent<Image>().material;
            m_DisplayedCharge = 0;
        }

        [UsedImplicitly]
        private void Update()
        {
            // Get main weapon's charge (or return if weapon is not chargeable)
            var em = Common.PlayerObj.GetComponent<EquipementManager>();
            var fcw = em.MainWeapon as FireChargeWeapon;
            if (fcw == null)
            {
                return;
            }

            // Time since a shot was fired
            var deltaT = Time.fixedTime - fcw.LastFireTime;

            // Decrease displayed charge after a shot is fired
            m_DisplayedCharge = fcw.IsFiring ? fcw.LastFireCharge : m_DisplayedCharge;
            m_DisplayedCharge = fcw.FireCharge < m_DisplayedCharge
                ? Mathf.Clamp01(m_DisplayedCharge -
                                fcw.LastFireCharge * UNCHARGE_RATE * Time.deltaTime)
                : fcw.FireCharge;

            // Update displayed charge
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            m_Material.SetFloat(CHARGE_SHADER_PROP_NAME, m_DisplayedCharge == 0
                ? 0
                : m_DisplayedCharge + MIN_CHARGE);

            // Firing animation
            var firingAnim = Mathf.Exp(-deltaT * 10);
            var chargingAnim = m_DisplayedCharge * FIRING_ANUM_SPEED;
            m_Material.SetFloat(RADIUS_SHADER_PROP_NAME, RADIUS +
                                                         FIRING_ANIM_RADIUS_AMPLITUDE * firingAnim -
                                                         chargingAnim);
            m_Material.SetFloat(THICKNESS_SHADER_PROP_NAME, THICKNESS +
                                                            FIRING_ANIM_THICNKESS_AMPLITUDE *
                                                            firingAnim - chargingAnim);

            // Color matching weapon mode
            if (fcw is EXIMG)
            {
                m_Material.SetColor(COLOR_SHADER_PROP_NAME, (fcw as EXIMG).GrenadeColor);
            }
        }

        #endregion
    }
}