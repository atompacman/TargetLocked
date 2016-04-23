using JetBrains.Annotations;
using UnityEngine;

namespace TargetLocked.Weapons
{
    public sealed class EquipementManager : MonoBehaviour
    {
        #region Fields

        public Weapon MainWeapon { get; private set; }

        #endregion

        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            var obj = new GameObject();
            MainWeapon = obj.AddComponent<EXIMG.EXIMG>();
            MainWeapon.transform.parent = transform;
        }

        [UsedImplicitly]
        private void Update()
        {
            // TODO: switch weapon
        }

        #endregion
    }
}