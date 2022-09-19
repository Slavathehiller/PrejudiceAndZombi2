using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class BottleOfWater : Food
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Бутылка воды";
        prefab = prefabsController.bottleOfWater;
        waterRestore = 50f;
    }

    public override void Consume()
    {
        if (itemRef.character is CharacterS)
        {   
            var bottle = ItemFactory.CreateItem(prefabsController.bottle).GetComponent<Bottle>();
            (itemRef.character as CharacterS).gameController.AddItemToPlayerSack(bottle.itemRef);
        }
        base.Consume();
    }
}
