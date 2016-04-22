using JetBrains.Annotations;

namespace TargetLocked.Weapons.GrenadeLaunchers
{
    public sealed class Exploder : GrenadeLauncher
    {
        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            GrenadeType = typeof(ExplosionGrenade);
        }

        #endregion Methods
    }
}