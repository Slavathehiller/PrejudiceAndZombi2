using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public abstract class Liquid : Consumable
{
    public abstract GameObject Container { get; }

    public override string ConsumeCaption => "Выпить";

    public override void Consume()
    {
        if (itemRef.character is CharacterS)
        {
            var container = ItemFactory.CreateItem(Container).GetComponent<Reservoir>();
            (itemRef.character as CharacterS).gameController.AddItemToPlayerSack(container.itemRef);
        }
        base.Consume();
    }
}
