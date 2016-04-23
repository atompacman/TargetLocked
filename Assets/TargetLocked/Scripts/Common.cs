using UnityEngine;

namespace TargetLocked
{
    public static class Common
    {
        #region Compile-time constants

        public const int LEFT_CLICK = 0;
        public const int MIDDLE_CLICK = 2;
        public const int RIGHT_CLICK = 1;
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