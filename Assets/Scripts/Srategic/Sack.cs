using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sack : MonoBehaviour
{
    public List<ItemReference> items = new List<ItemReference>();

    public void AddItem(ItemReference item)
    {
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
