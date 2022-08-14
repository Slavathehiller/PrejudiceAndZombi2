using Assets.Scripts.Interchange;
using System.Collections.Generic;

public enum StateOnLoad
{
    StartGame  = 0,
    LoadFromStrategy = 1,
    LoadFromTactic = 2
}
public static class Global
{
    public static StateOnLoad lastStateOnLoad = StateOnLoad.StartGame;
    public static CharacterTransferData character;
    public static List<ItemTransferData> Loot;
    public static LocationTransferData locationTransferData;

    public static void ReloadCharacter(ICharacter character)
    {

        Item CheckAndInst(ItemTransferData data)
        {
            var item = Item.RestoreFromDTO(data, null, character);
            if (item != null)
            {
                item.itemRef.gameObject.SetActive(true);
                item.gameObject.SetActive(false);
            }
            return item;
        }

        character.Stats = Global.character.Stats;
        var inventory = character.inventory;
        inventory.EquipItem(CheckAndInst(Global.character.Inventory.shirt) as EquipmentItem, SpecType.EqShirt);
        inventory.EquipItem(CheckAndInst(Global.character.Inventory.belt) as EquipmentItem, SpecType.EqBelt);
        inventory.EquipItem(CheckAndInst(Global.character.Inventory.pants) as EquipmentItem, SpecType.EqPants);
        inventory.EquipItem(CheckAndInst(Global.character.Inventory.helmet) as ArmorItem, SpecType.Helmet);
        inventory.EquipItem(CheckAndInst(Global.character.Inventory.chestArmor) as ArmorItem, SpecType.ChestArmor);
        inventory.EquipItem(CheckAndInst(Global.character.Inventory.gloves) as ArmorItem, SpecType.Gloves);
        inventory.EquipItem(CheckAndInst(Global.character.Inventory.boots) as ArmorItem, SpecType.Boots);

        var RHItem = CheckAndInst(Global.character.RightHand);
        if (RHItem != null)
            inventory.TakeItem(RHItem.itemRef);

        var RSItem = CheckAndInst(Global.character.Inventory.rightShoulder);
        if (RSItem != null)
            inventory.TossOverItem(RSItem.itemRef);

        var LSItem = CheckAndInst(Global.character.Inventory.leftShoulder);
        if (LSItem != null)
            inventory.TossOverItem(LSItem.itemRef, true);

    }
}
