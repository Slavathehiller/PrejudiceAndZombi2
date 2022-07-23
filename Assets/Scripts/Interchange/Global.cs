using Assets.Scripts.Interchange;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static bool needToLoad = false;
    public static CharacterTransferData character;
    public static List<ItemTransferData> Loot;


    public static void ReloadCharacter(ICharacter character)
    {

        Item CheckAndInst(ItemTransferData data)
        {
            if (data is null)
                return null;
            var item = Object.Instantiate(data.Prefab).GetComponent<TacticalItem>();
            item.SetCount(data.Count);

            if (data is RangedWeaponTransferData)
            {
                var _data = data as RangedWeaponTransferData;
                Object.Destroy((item as RangedWeapon).magazine.gameObject);
                if (_data.Magazine is null)
                {
                    (item as RangedWeapon).magazine = null;
                }
                else
                {
                    (item as RangedWeapon).magazine = CheckAndInstMag(_data.Magazine);
                    (item as RangedWeapon).magazine.transform.SetParent((item as RangedWeapon).magazinePoint.transform);
                    (item as RangedWeapon).magazine.transform.localPosition = Vector3.zero;
                }
                item.itemRef.ShowUnloadButton(false);
            }

            if (data is WeaponMagazineTransferData)
            {
                (item as WeaponMagazine).CurrentAmmoCount = (data as WeaponMagazineTransferData).CurrentAmmoCount;
                (item as WeaponMagazine).CurrentAmmoData = (data as WeaponMagazineTransferData).CurrentAmmoData;
            }

            item.itemRef.character = character;
            item.itemRef.gameObject.SetActive(true);
            item.gameObject.SetActive(false);
            return item;
        }

        WeaponMagazine CheckAndInstMag(WeaponMagazineTransferData data)
        {
            var magazine = Object.Instantiate(data.Prefab).GetComponent<WeaponMagazine>();
            magazine.CurrentAmmoCount = data.CurrentAmmoCount;
            magazine.CurrentAmmoData = data.CurrentAmmoData;
            magazine.gameObject.SetActive(false);
            return magazine;
        }

        EquipmentItem CheckAndInstEq(EquipmentItemTransferData data)
        {
            if (data is null)
                return null;
            else
            {
                var item = Object.Instantiate(data.Prefab).GetComponent<EquipmentItem>();
                item.itemRef.character = character;
                var invItem = item as InventoryEquipmentItem;
                if (invItem != null)
                {
                    var i = 0;
                    foreach (var itemdata in data.ItemList)
                    {
                        if (itemdata != null)
                        {
                            var tac_item = CheckAndInst(itemdata);
                            invItem.cellList[i].PlaceItemToCell(tac_item.itemRef);
                            if (tac_item is RangedWeapon)
                                tac_item.itemRef.ShowUnloadButton(false);
                         }
                        i++;
                    }
                    item.itemRef.gameObject.SetActive(character is CharacterS);
                    return invItem;
                }

                item.itemRef.gameObject.SetActive(character is CharacterS);
                return item;
            }
        }

        EquipmentItem CheckAndInstAr(ItemTransferData data)
        {
            if (data is null)
                return null;
            else
            {
                var item = Object.Instantiate(data.Prefab).GetComponent<EquipmentItem>();
                item.itemRef.character = character;
                item.itemRef.gameObject.SetActive(character is CharacterS);
                return item;
            }
        }

        character.Stats = Global.character.Stats;
        var inventory = character.inventory;
        inventory.EquipItem(CheckAndInstEq(Global.character.Inventory.shirt), SpecType.EqShirt);
        inventory.EquipItem(CheckAndInstEq(Global.character.Inventory.belt), SpecType.EqBelt);
        inventory.EquipItem(CheckAndInstEq(Global.character.Inventory.pants), SpecType.EqPants);
        inventory.EquipItem(CheckAndInstAr(Global.character.Inventory.helmet), SpecType.Helmet);
        inventory.EquipItem(CheckAndInstAr(Global.character.Inventory.chestArmor), SpecType.ChestArmor);
        inventory.EquipItem(CheckAndInstAr(Global.character.Inventory.gloves), SpecType.Gloves);
        inventory.EquipItem(CheckAndInstAr(Global.character.Inventory.boots), SpecType.Boots);

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
