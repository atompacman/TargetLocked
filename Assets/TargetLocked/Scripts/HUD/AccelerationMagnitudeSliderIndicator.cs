using UnityEngine;

namespace TargetLocked.HUB
{
    public sealed class AccelerationMagnitudeSliderIndicator : SliderIndicator
    {
        #region Compile-time constants

        private const float DECREASE_RATE = 3f;
        private const float DECREASE_ACCEL_RATE = 1f;
        private const float MAX_VALUE = 80f;

        #endregion Compile-time constants

        #region Runtime constants

        private static readonly Color MAX_COLOR = Color.magenta;

        #endregion Runtime constants

        #region Fields

        private Vector3 m_PrevAcceleration;
        private float m_PrevValue;
        private float m_StartDecreasingTime;

        #endregion Fields

        #region Methods

        protected override float GetCurrentValue()
        {
            // Compute current acceleration
            var curr = Common.PlayerObj.GetComponent<Rigidbody>().velocity;
            var prev = m_PrevAcceleration;
            m_PrevAcceleration = curr;
            var value = (prev - curr).magnitude;

            // If acceleration is decreasing, slowly decrease displayed value
            if (value < m_PrevValue)
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (m_StartDecreasingTime == 0)
                {
                    m_StartDecreasingTime = Time.fixedTime;
                }

                value = Mathf.Max(0,
                    m_PrevValue -
                    Time.deltaTime * DECREASE_RATE *
                    Mathf.Exp((Time.fixedTime - m_StartDecreasingTime) * DECREASE_ACCEL_RATE));
            }
            else
            {
                m_StartDecreasingTime = 0;
            }

            m_PrevValue = value;
            return value;
        }

        protected override void Init()
        {
            MaxValue = MAX_VALUE;
            MaxColor = MAX_COLOR;

            m_PrevAcceleration = Vector3.zero;
            m_PrevValue = 0;
            m_StartDecreasingTime = 0;
        }

        #endregion Methods
    }
}