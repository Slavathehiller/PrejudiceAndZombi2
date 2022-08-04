using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Hammer : Tool
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Молоток";
        prefab = prefabsController.hammer;
    }
}
