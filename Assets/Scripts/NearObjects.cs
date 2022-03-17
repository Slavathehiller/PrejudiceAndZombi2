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
    public void AddThing(TacticalItem thing, Character character)
    {
        var pref = Instantiate(pfController.thingRef, groundPanel.transform);
        var refThing = pref.GetComponent<ItemReference>();
        refThing.image.sprite = thing.image;
        refThing.thing = thing.gameObject;
        thing.thingRef = refThing;
        refThing.character = character;

        list.Add(refThing);
    }

    public void DeleteThing(ItemReference thing, bool deleteGameObject = false)
    {
        thing.thing.GetComponent<TacticalItem>().thingRef = null;
        list.Remove(thing);
        if (deleteGameObject)
        {
            Destroy(thing.gameObject);
        }
    }
}
