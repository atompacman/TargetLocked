using UnityEngine;

namespace TargetLocked.HUD
{
    public sealed class VelocityMagnitudeSliderIndicator : SliderIndicator
    {
        #region Compile-time constants

        private const float MAX_VALUE = 80f;

        #endregion

        #region Runtime constants

        private static readonly Color MAX_COLOR = Color.red;

        #endregion

        #region Methods

        protected override void Init()
        {
            MaxValue = MAX_VALUE;
            MaxColor = MAX_COLOR;
        }

        protected override float GetCurrentValue()
        {
            return Common.PlayerObj.GetComponent<Rigidbody>().velocity.magnitude;
        }

        #endregion
    }
}