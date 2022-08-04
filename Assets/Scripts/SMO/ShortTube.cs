using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ShortTube : Item
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Короткая труба";
        prefab = prefabsController.shortTube;
    }
}

