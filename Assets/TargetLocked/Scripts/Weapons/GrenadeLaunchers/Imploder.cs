using JetBrains.Annotations;

namespace TargetLocked.Weapons.GrenadeLaunchers
{
    public sealed class Imploder : GrenadeLauncher
    {
        #region Methods

        [UsedImplicitly]
        private void Start()
        {
            GrenadeType = typeof(ImplosionGrenade);
        }

        #endregion Methods
    }
}