using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class RangedWeaponTransferData: ItemTransferData
    {
        public WeaponMagazineTransferData Magazine { get; set; }

        public override Item Restore(ICharacter character)
        {
            var obj = base.Restore(character);
            var item = obj.GetComponent<RangedWeapon>();
            item.magazine.CurrentAmmoCount = Magazine.CurrentAmmoCount;
            item.magazine.CurrentAmmoData = Magazine.CurrentAmmoData;
         //   item.magazine.gameObject.SetActive(false);


            return item;
        }
    }
}
