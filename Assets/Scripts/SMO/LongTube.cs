using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LongTube : Item
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Длинная труба";
        prefab = prefabsController.longTube;
    }
}

