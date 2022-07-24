using UnityEngine;

public class ArmorItem : EquipmentItem
{   
    public float armor;
    public override void PlaceItemToSack(GameObject sack)
    {
        var cell = itemRef.oldParent.GetComponent<ArmorCell>();
        if (cell != null)
            cell.armorText.text = "0%";
        itemRef.character.inventory.EquipItem(null, specType);
        base.PlaceItemToSack(sack);
    }
}
