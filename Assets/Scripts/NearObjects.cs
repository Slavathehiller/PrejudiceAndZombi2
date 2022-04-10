using Assets.Scripts.Entity;
using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NearObjects : MonoBehaviour
{
    public List<ItemReference> list = new List<ItemReference>();
    public PrefabsController pfController;
    public GameObject groundPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(TacticalItem item, Character character)
    {
        var pref = Instantiate(pfController.thingRef, groundPanel.transform);
        var refItem = pref.GetComponent<ItemReference>();
        refItem.image.sprite = item.image;
        refItem.thing = item.gameObject;
        item.itemRef = refItem;
        refItem.character = character;

        list.Add(refItem);
    }

    public void DeleteThing(ItemReference item, bool deleteGameObject = false)
    {
        //item.thing.GetComponent<TacticalItem>().itemRef = null;
        list.Remove(item);
        if (deleteGameObject)
        {
            Destroy(item.gameObject);
        }
    }
}
