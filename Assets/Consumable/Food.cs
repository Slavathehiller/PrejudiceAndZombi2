using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Consumable
{
    public float foodRestore { get; set; }
    public float waterRestore { get; set; }
    public override void Consume()
    {
        ((CharacterS)itemRef.character).Food += foodRestore;
        ((CharacterS)itemRef.character).Water += waterRestore;
        Destroy(itemRef.gameObject);
        Destroy(gameObject);
    }
}
