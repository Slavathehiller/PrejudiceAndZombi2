using Assets.Scripts.Entity;
using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NearObjects : MonoBehaviour
{
    public List<ThingReference> list = new List<ThingReference>();
    public PrefabsController pfController;
    public GameObject groundPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddThing(BaseWeapon thing, Character character)
    {
        var pref = Instantiate(pfController.thingRef, groundPanel.transform);
        var refThing = pref.GetComponent<ThingReference>();
        refThing.image.sprite = thing.image;
        refThing.thing = thing.gameObject;
        thing.thingRef = refThing;
        refThing.character = character;
        list.Add(refThing);
    }

    public void DeleteThing(ThingReference thing)
    {
        thing.thing.GetComponent<BaseWeapon>().thingRef = null;
        list.Remove(thing);
        Destroy(thing.gameObject);
    }
}
