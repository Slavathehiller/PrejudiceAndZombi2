using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThingReference : MonoBehaviour
{
    public GameObject thing;
    public Image image;
    public Character character;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickUp()
    {
        character.pcontroller.PickUpItem(this);
    }
}
