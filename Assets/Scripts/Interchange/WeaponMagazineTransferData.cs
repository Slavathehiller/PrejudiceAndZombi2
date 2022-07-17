using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class WeaponMagazineTransferData : ItemTransferData
    {
        public AmmoData CurrentAmmoData { get; set; }
        public int CurrentAmmoCount { get; set; }
    }
}
