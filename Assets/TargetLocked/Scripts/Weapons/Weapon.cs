using JetBrains.Annotations;
using UnityEngine;

namespace TargetLocked.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        #region Compile-time constants

        public const string ASSET_DIR = "Weapons/";

        #endregion

        #region Abstract methods

        public abstract void Fire();

        #endregion
    }
}