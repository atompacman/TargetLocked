using System;
using JetBrains.Annotations;
using UnityEngine;

namespace TargetLocked.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        #region Nested types

        public enum Mode
        {
            MAIN,
            ALTERNATIVE
        }

        #endregion

        #region Compile-time constants

        public const string ASSET_DIR = "Weapons/";

        #endregion

        #region Fields

        private bool m_IsHoldingTrigger;

        #endregion

        #region Properties

        public bool IsFiring { get; protected set; }
        public float LastFireTime { get; protected set; }

        public Mode GunMode { get; private set; }
        public WeaponInfoAttribute Info { get; private set; }

        #endregion

        #region Abstract methods

        protected abstract void OnTriggerHeld();

        protected abstract void OnTriggerPulled();

        protected abstract void OnTriggerReleased();

        #endregion

        #region Static methods

        public static string GetAssetDir<T>()
        {
            return ASSET_DIR + GetWeaponInfo<T>().Name + "/";
        }

        public static WeaponInfoAttribute GetWeaponInfo<T>()
        {
            return GetWeaponInfo(typeof(T));
        }

        public static WeaponInfoAttribute GetWeaponInfo(Type i_WeaponType)
        {
            var attr = i_WeaponType.GetCustomAttributes(typeof(WeaponInfoAttribute), true);
            return (WeaponInfoAttribute) attr[0];
        }

        #endregion

        #region Methods

        [UsedImplicitly]
        protected virtual void Start()
        {
            m_IsHoldingTrigger = false;
            IsFiring = false;
            GunMode = Mode.MAIN;
            LastFireTime = float.NegativeInfinity;
            Info = GetWeaponInfo(GetType());
            name = Info.Name;
        }

        [UsedImplicitly]
        private void Update()
        {
            IsFiring = false;

            // Check if trigger is pulled
            if (Input.GetMouseButtonDown(Common.LEFT_CLICK))
            {
                OnTriggerPulled();
                m_IsHoldingTrigger = true;
            }

            // Update if trigger is held
            if (m_IsHoldingTrigger)
            {
                OnTriggerHeld();
            }

            // Check is trigger is released
            if (Input.GetMouseButtonUp(Common.LEFT_CLICK))
            {
                OnTriggerReleased();
                m_IsHoldingTrigger = false;
            }

            // Check if weapon is switched
            if (Input.GetMouseButtonDown(Common.MIDDLE_CLICK))
            {
                GunMode = GunMode == Mode.MAIN ? Mode.ALTERNATIVE : Mode.MAIN;
            }
        }

        #endregion
    }
}