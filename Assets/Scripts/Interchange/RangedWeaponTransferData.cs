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

        public override Item Restore(GameObject parent, ICharacter character)
        {
            var obj = base.Restore(parent, character);
            var item = obj.GetComponent<RangedWeapon>();
            item.magazine.CurrentAmmoCount = Magazine.CurrentAmmoCount;
            item.magazine.CurrentAmmoData = Magazine.CurrentAmmoData;

            return item;
        }
    }
}
