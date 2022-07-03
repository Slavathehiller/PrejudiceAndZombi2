using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<TacticalItem> tacticalItems;
    public EquipmentItem shirt;
    public EquipmentItem pants;
    public EquipmentItem belt;
    public EquipmentItem helmet;
    public EquipmentItem chestArmor;
    public EquipmentItem gloves;
    public EquipmentItem boots;

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


    // Start is called before the first frame update
    void Start()
    {
        tacticalItems = new List<TacticalItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(TacticalItem item)
    {
        if(!tacticalItems.Contains(item))
            tacticalItems.Add(item);
    }

    public void TryRemoveItem(Item item)
    {
        if (item is TacticalItem)
            tacticalItems.Remove(item as TacticalItem);
        if (item is EquipmentItem && getEquipmentItem(((EquipmentItem)item).specType) == item)
            UnEquipItem(((EquipmentItem)item).specType);
    }

    public bool IsCellEmpty(ItemCell cell)
    {
        return ItemInCell(cell) == null;
    }


    public TacticalItem ItemInCell(ItemCell cell)
    {
        foreach (var item in tacticalItems)
        {
            if (item.itemRef.gameObject.transform.parent == cell.gameObject.transform)
            {
                return item;
            }
        }
        return null;
    }

    public void EquipItem(EquipmentItem EqItem)
    {
        switch (EqItem.specType)
        {
            case SpecType.EqShirt:
                shirt = EqItem;
                break;
            case SpecType.EqBelt:
                belt = EqItem;
                break;
            case SpecType.EqPants:
                pants = EqItem;
                break;
            case SpecType.Helmet:
                helmet = EqItem;
                break;
            case SpecType.ChestArmor:
                chestArmor = EqItem;
                break;
            case SpecType.Boots:
                boots = EqItem;
                break;
            case SpecType.Gloves:
                gloves = EqItem;
                break;
            default:
                throw new Exception("Неверный тип");
        }
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
