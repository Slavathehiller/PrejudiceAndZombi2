using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SMO : TacticalItem
{
    [SerializeField]
    protected int _count;

    public int Count
    {
        get
        {
            return _count;
        }
    }
    public void Add(int number = 1)
    {
        _count += number;
        if (itemRef != null)
            itemRef.count.text = _count.ToString();
        if (_count < 1)
        {
            itemRef.character.inventory.RemoveItem(this);
            Destroy(itemRef.gameObject);
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if(itemRef != null)
            itemRef.count.text = _count.ToString();
    }

}
