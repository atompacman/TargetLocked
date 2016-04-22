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

        #endregion Nested types

        #region Compile-time constants

        public const string ASSET_DIR = "Weapons/";

        #endregion Compile-time constants

        #region Fields

        protected Mode GunMode;
        private bool m_IsHoldingTrigger;

        #endregion Fields

        #region Abstract methods

        protected abstract void OnTriggerPulled();

        protected abstract void OnTriggerHeld();

        protected abstract void OnTriggerReleased();

        #endregion Abstract methods

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            GunMode = Mode.MAIN;
            m_IsHoldingTrigger = false;
        }

        [UsedImplicitly]
        private void Update()
        {
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
            if (Input.GetMouseButtonUp(Common.MIDDLE_CLICK))
            {
                GunMode = GunMode == Mode.MAIN ? Mode.ALTERNATIVE : Mode.MAIN;
            }
        }

        #endregion Methods
    }
}