using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleOfWater : Liquid
{
    public override GameObject Container => prefabsController.bottle;

    protected override void Awake()
    {
        base.Awake();
        Name = "Бутылка воды";
        prefab = prefabsController.bottleOfWater;
        waterRestore = 50f;
    }

    
}
