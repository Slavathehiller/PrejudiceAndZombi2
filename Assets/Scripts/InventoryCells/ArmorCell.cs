using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorCell : EquipmentCell
{
    public Text armorText;
    public override void PlaceItemToCell(ItemReference item)
    {
        base.PlaceItemToCell(item);
        armorText.text = item.thing.GetComponent<ArmorItem>().armor.ToString() + "%";
        //gameController.RefreshArmorText();
    }
}
