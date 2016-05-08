using UnityEngine;

namespace TargetLocked
{
    public static class Common
    {
        #region Compile-time constants

        public const string FIRE_BUTTON = "Fire";
        public const string SWITCH_WEAPON_MODE_BUTTON = "Switch Weapon Mode";
        public const string RUN_BUTTON = "Run";

        public const float EYES_HEIGHT = 1.5f;
        private const string PLAYER_OBJ_NAME = "RigidBodyFPSController";

        #endregion Compile-time constants

        #region Static fields

        private static GameObject s_PlayerObj;

        #endregion Static fields

        #region Properties

        public static GameObject PlayerObj
        {
            get
            {
                // ReSharper disable once ConvertPropertyToExpressionBody (not supported in Unity)
                return s_PlayerObj ?? (s_PlayerObj = GameObject.Find(PLAYER_OBJ_NAME));
            }
        }

        #endregion Properties
    }
}