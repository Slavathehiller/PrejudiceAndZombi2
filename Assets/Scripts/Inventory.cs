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
                boots = this.boots is null ? null : (this.boots as ArmorItem).TransferData
            };
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryRemoveItem(Item item)
    {
        if (item is TacticalItem)
        {
            (shirt as InventoryEquipmentItem).TryRemoveItem(item);
            (pants as InventoryEquipmentItem).TryRemoveItem(item);
            (belt as InventoryEquipmentItem).TryRemoveItem(item);
        }
    }

    //public bool IsCellEmpty(ItemCell cell)
    //{
    //    return ItemInCell(cell) == null;
    //}

    //public TacticalItem ItemInCell(ItemCell cell)
    //{
    //    foreach (var item in tacticalItems)
    //    {
    //        if (item.itemRef.gameObject.transform.parent == cell.gameObject.transform)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}

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
                break;
            case SpecType.EqBelt:
                belt = EqItem;
                SetCellSchema(belt, beltContainer);
                break;
            case SpecType.EqPants:
                pants = EqItem;
                SetCellSchema(pants, pantsContainer);
                break;
            case SpecType.Helmet:
                helmet = EqItem;
                SetArmorText();
                break;
            case SpecType.ChestArmor:
                chestArmor = EqItem;
                SetArmorText();
                break;
            case SpecType.Boots:
                boots = EqItem;
                SetArmorText();
                break;
            case SpecType.Gloves:
                gloves = EqItem;
                SetArmorText();
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

    public void UnEquipItem(SpecType specType)
    {
        switch (specType)
        {
            case SpecType.EqShirt:
                shirt = null;
                break;
            case SpecType.EqBelt:
                belt = null;
                break;
            case SpecType.EqPants:
                pants = null;
                break;
            case SpecType.Helmet:
                helmet = null;
                break;
            case SpecType.ChestArmor:
                chestArmor = null;
                break;
            case SpecType.Boots:
                boots = null;
                break;
            case SpecType.Gloves:
                gloves = null;
                break;
            default:
                throw new Exception("Неверный тип");
        }
    }

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
