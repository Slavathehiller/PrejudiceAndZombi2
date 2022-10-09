using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item
{
    public abstract string ConsumeCaption { get;}

    public float foodRestore { get; set; }
    public float waterRestore { get; set; }

    public override string StatsInfo 
    { 
        get
        {
            string result = "";
            if (foodRestore > 0)
                result = "Еда: " + foodRestore + "\n";
            if(waterRestore > 0)
                result += "Вода: " + waterRestore + "\n";
            return result;
        }
    }

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
            result.Add(PopupController.CreateMenuPointData(ConsumeCaption, Consume, null));
            return result;
        }
    }

}
