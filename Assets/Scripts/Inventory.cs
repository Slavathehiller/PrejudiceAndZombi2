using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<TacticalItem> list;

    // Start is called before the first frame update
    void Start()
    {
        list = new List<TacticalItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(TacticalItem item)
    {
        if(!list.Contains(item))
            list.Add(item);
    }

    public void RemoveItem(TacticalItem item)
    {
        list.Remove(item);
    }

    public bool IsCellEmpty(ItemCell cell)
    {
        return ItemInCell(cell) == null;
    }

    public TacticalItem ItemInCell(ItemCell cell)
    {
        foreach (var item in list)
        {
            if (item.itemRef.gameObject.transform.parent == cell.gameObject.transform)
            {
                return item;
            }
        }
        return null;
    }

}
