using Assets.Scripts.Interchange;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sack : MonoBehaviour
{
    public List<ItemReference> items = new List<ItemReference>();

    public void AddItem(ItemReference item)
    {
        if(!Contains(item))
            items.Add(item);
    }

    public void RemoveItem(ItemReference item)
    {
        items.Remove(item);
    }

    public bool Contains(ItemReference item)
    {
        return items.Contains(item);
    }

    public List<ItemTransferData> TransferData
    {
        get
        {
            return items.Select(x => x.Item.TransferData).ToList();
        }
    }
}
