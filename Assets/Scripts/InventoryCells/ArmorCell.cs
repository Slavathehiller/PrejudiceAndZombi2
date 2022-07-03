using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorCell : EquipmentCell
{
    public Text armorText;
    public override void PlaceItemToCell(EquipmentItem thing)
    {
        base.PlaceItemToCell(thing);
        armorText.text = ((ArmorItem)thing).armor.ToString() + "%";
        gameController.RefreshArmorText();
    }


}
