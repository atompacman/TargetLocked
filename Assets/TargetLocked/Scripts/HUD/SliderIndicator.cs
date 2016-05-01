using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace TargetLocked.HUD
{
    public abstract class SliderIndicator : MonoBehaviour
    {
        #region Compile-time constants

        protected const int MAX_CURSOR_POS = 94;
        protected const int MIN_CURSOR_POS = -194;
        private const string CURSOR_OBJ_NAME = "Cursor";
        private const string LINE_OBJ_NAME = "Line";

        #endregion

        #region Fields

        protected Transform Cursor;
        protected Image Line;
        protected Color MaxColor;
        protected float MaxValue;

        #endregion

        #region Abstract methods

        protected abstract float GetCurrentValue();

        protected abstract void Init();

        #endregion

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            Cursor = transform.FindChild(CURSOR_OBJ_NAME);
            Line = transform.FindChild(LINE_OBJ_NAME).GetComponent<Image>();

            Init();
        }

        [UsedImplicitly]
        private void Update()
        {
            // Compute normalized value to display
            var value = Mathf.Min(GetCurrentValue(), MaxValue) / MaxValue;

            // Set cursor position
            var pos = Cursor.localPosition;
            pos.x = MIN_CURSOR_POS + value * (MAX_CURSOR_POS - MIN_CURSOR_POS);
            Cursor.localPosition = pos;

            // Set line and cursor color
            var color = Color.Lerp(Color.white, MaxColor, value);
            Line.color = color;
            Cursor.GetComponent<Image>().color = color;
        }

        #endregion
    }
}