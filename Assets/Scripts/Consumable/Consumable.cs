using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item
{
    public abstract string CosumeCaption { get;}

    public float foodRestore { get; set; }
    public float waterRestore { get; set; }

    public virtual void Consume()
    {
        ((CharacterS)itemRef.character).Food += foodRestore;
        ((CharacterS)itemRef.character).Water += waterRestore;
        Destroy(itemRef.gameObject);
        Destroy(gameObject);
    }

    public override List<MenuPointData> menuCommands
    {
        get
        {
            var result = base.menuCommands;
            result.Add(PopupController.CreateMenuPointData(CosumeCaption, Consume, null));
            return result;
        }
    }

}
