using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class WeaponMagazineTransferData : ItemTransferData
    {
        public AmmoData CurrentAmmoData { get; set; }
        public int CurrentAmmoCount { get; set; }

        public override Item Restore(GameObject parent, ICharacter character)
        {
            var obj = base.Restore(parent, character);
            var item = obj.GetComponent<WeaponMagazine>();
            item.CurrentAmmoData = CurrentAmmoData;
            item.CurrentAmmoCount = CurrentAmmoCount;
            return item;
        }
    }
}
