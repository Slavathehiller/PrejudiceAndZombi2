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
            default:
                throw new Exception("Неверный тип");
        }
    }

}
