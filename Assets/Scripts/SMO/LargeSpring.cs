using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LargeSpring : Item
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Большая пружина";
        prefab = prefabsController.largeSpring;
    }
}

