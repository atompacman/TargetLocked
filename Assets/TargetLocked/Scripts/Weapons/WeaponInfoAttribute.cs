using System;

namespace TargetLocked.Weapons
{
    public sealed class WeaponInfoAttribute : Attribute
    {
        #region Constructors

        public WeaponInfoAttribute(string i_Name, string i_MainModeName, string i_AltModeName)
        {
            Name = i_Name;
            MainModeName = i_MainModeName;
            AltModeName = i_AltModeName;
        }

        #endregion

        #region Properties

        public string AltModeName { get; private set; }
        public string MainModeName { get; private set; }
        public string Name { get; private set; }

        #endregion
    }
}