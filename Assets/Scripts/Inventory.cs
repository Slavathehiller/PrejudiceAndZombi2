using Assets.Scripts.Interchange;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public EquipmentItem shirt;
    public EquipmentItem pants;
    public EquipmentItem belt;
    public EquipmentItem helmet;
    public EquipmentItem chestArmor;
    public EquipmentItem gloves;
    public EquipmentItem boots;

    public GameObject shirtContainer;
    public GameObject pantsContainer;
    public GameObject beltContainer;

    public EquipmentCell shirtCell;
    public EquipmentCell pantsCell;
    public EquipmentCell beltCell;

    public ArmorCell helmetCell;
    public ArmorCell chestArmorCell;
    public ArmorCell glovesCell;
    public ArmorCell bootsCell;

    public RightHandCell rightHandCell;

    public ItemCell rightShoulderCell;
    public ItemCell leftShoulderCell;

    public Text ArmorText;

    public float HelmetArmor 
    {
        get
        {
            if (helmet != null)
                return ((ArmorItem)helmet).armor;
            else
                return 0;
        }
    }
    public float ChestArmor
    {
        get
        {
            if (chestArmor != null)
                return ((ArmorItem)chestArmor).armor;
            else
                return 0;
        }
    }
    public float GlovesArmor
    {
        get
        {
            if (gloves != null)
                return ((ArmorItem)gloves).armor;
            else
                return 0;
        }
    }
    public float BootsArmor
    {
        get
        {
            if (boots != null)
                return ((ArmorItem)boots).armor;
            else
                return 0;
        }
    }

    public float Armor
    {
        get
        {
            return (HelmetArmor + ChestArmor + GlovesArmor + BootsArmor) / 4f;
        }
    }

    public InventoryTransferData TransferData
    {
        get
        {
            return new InventoryTransferData
            {
                shirt = this.shirt is null ? null : (this.shirt as InventoryEquipmentItem).TransferData,
                belt = this.belt is null ? null : (this.belt as InventoryEquipmentItem).TransferData,
                pants = this.pants is null ? null : (this.pants as InventoryEquipmentItem).TransferData,
                helmet = this.helmet is null ? null : (this.helmet as ArmorItem).TransferData,
                chestArmor = this.chestArmor is null ? null : (this.chestArmor as ArmorItem).TransferData,
                gloves = this.gloves is null ? null : (this.gloves as ArmorItem).TransferData,
                boots = this.boots is null ? null : (this.boots as ArmorItem).TransferData,
                rightShoulder = this.rightShoulderCell.itemIn is null ? null : this.rightShoulderCell.itemIn.TransferData,
                leftShoulder = this.leftShoulderCell.itemIn is null ? null : this.leftShoulderCell.itemIn.TransferData
            };
        }
    }

    public void TryRemoveItem(Item item)
    {
        if (item is TacticalItem)
        {
            if (shirt != null)
                (shirt as InventoryEquipmentItem).TryRemoveItem(item);
            if (pants != null)
                (pants as InventoryEquipmentItem).TryRemoveItem(item);
            if (belt != null)
                (belt as InventoryEquipmentItem).TryRemoveItem(item);
        }
    }

    public void TakeItem(ItemReference item)
    {
        rightHandCell.PlaceItemToCell(item);
    }

    public void TossOverItem(ItemReference item, bool left = false)
    {
        if (left)
        {
            leftShoulderCell.PlaceItemToCell(item);
        }
        else
            rightShoulderCell.PlaceItemToCell(item);
    }

    public void UnEquipItem(SpecType specType)
    {
        EquipItem(null, specType);
    }

    public void EquipItem(EquipmentItem EqItem, SpecType defType = 0)
    {
        SpecType _specType;
        if (defType == 0)
            _specType = EqItem.specType;
        else
            _specType = defType;

        switch (_specType)
        {
            case SpecType.EqShirt:
                shirt = EqItem;
                SetCellSchema(shirt, shirtContainer);
                if (shirtCell != null)
                    shirtCell.PlaceItemToCell(shirt);
                break;
            case SpecType.EqBelt:
                belt = EqItem;
                SetCellSchema(belt, beltContainer);
                if (beltCell != null)
                    beltCell.PlaceItemToCell(belt);
                break;
            case SpecType.EqPants:
                pants = EqItem;
                SetCellSchema(pants, pantsContainer);
                if (pantsCell != null)
                    pantsCell.PlaceItemToCell(pants);
                break;
            case SpecType.Helmet:
                helmet = EqItem;
                SetArmorText();
                if (helmetCell != null)
                    helmetCell.PlaceItemToCell(helmet);
                break;
            case SpecType.ChestArmor:
                chestArmor = EqItem;
                SetArmorText();
                if (chestArmorCell != null)
                    chestArmorCell.PlaceItemToCell(chestArmor);
                break;
            case SpecType.Boots:
                boots = EqItem;
                SetArmorText();
                if (bootsCell != null)
                    bootsCell.PlaceItemToCell(boots);
                break;
            case SpecType.Gloves:
                gloves = EqItem;
                SetArmorText();
                if (glovesCell != null)
                    glovesCell.PlaceItemToCell(gloves);
                break;
            default:
                throw new Exception("Неверный тип");
        }
    }

    private void SetCellSchema(EquipmentItem item, GameObject container)
    {
        if (item is null)
            return;
        var scheme = ((InventoryEquipmentItem)item).cellScheme;
        scheme.transform.SetParent(container.transform);
        scheme.transform.localPosition = Vector3.zero;
        scheme.transform.localScale = new Vector3(1, 1, 1);
        scheme.SetActive(true);
    }

    private void SetArmorText()
    {
        ArmorText.text = "Броня " + Armor.ToString() + " %";
    }

    //public void UnEquipItem(SpecType specType)
    //{
    //    switch (specType)
    //    {
    //        case SpecType.EqShirt:
    //            shirt = null;
    //            break;
    //        case SpecType.EqBelt:
    //            belt = null;
    //            break;
    //        case SpecType.EqPants:
    //            pants = null;
    //            break;
    //        case SpecType.Helmet:
    //            helmet = null;
    //            break;
    //        case SpecType.ChestArmor:
    //            chestArmor = null;
    //            break;
    //        case SpecType.Boots:
    //            boots = null;
    //            break;
    //        case SpecType.Gloves:
    //            gloves = null;
    //            break;
    //        default:
    //            throw new Exception("Неверный тип");
    //    }
    //}

    public EquipmentItem getEquipmentItem(SpecType specType)
    {
        switch (specType) 
        {
            case SpecType.EqShirt:
                return shirt;
            case SpecType.EqBelt:
                return belt;
            case SpecType.EqPants:
                return pants;
            case SpecType.Helmet:
                return helmet;
            case SpecType.ChestArmor:
                return chestArmor;
            case SpecType.Boots:
                return boots;
            case SpecType.Gloves:
                return gloves;
            default:
                throw new Exception("Неверный тип");
        }
    }

}
