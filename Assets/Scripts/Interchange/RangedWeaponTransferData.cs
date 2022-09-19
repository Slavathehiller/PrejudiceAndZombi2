using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class RangedWeaponTransferData: ItemTransferData
    {
        public WeaponMagazineTransferData Magazine { get; set; }

        public override Item Restore(GameObject parent = null, ICharacter character = null)
        {
            var obj = base.Restore(parent, character);
            var item = obj.GetComponent<RangedWeapon>();
            GameObject.Destroy(item.magazine.gameObject);
            if (Magazine != null)
            {
                var newMagazine = Magazine.Restore(null, character) as WeaponMagazine;
                newMagazine.CurrentAmmoCount = Magazine.CurrentAmmoCount;
                newMagazine.CurrentAmmoData = Magazine.CurrentAmmoData;
                if (newMagazine.extractable)
                {
                    item.Reload(newMagazine);
                    //item.itemRef.ShowUnloadButton(false);
                }
                else
                    item.magazine = newMagazine;
            }
            else
            {
                item.magazine = null;
            }

            return item;
        }
    }
}
