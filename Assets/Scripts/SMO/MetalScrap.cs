using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MetalScrap : Item
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Металлолом";
        prefab = prefabsController.metalScrap;
    }
}
