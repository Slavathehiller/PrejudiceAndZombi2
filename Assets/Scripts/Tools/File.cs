using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class File : Tool
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Напильник";
        prefab = prefabsController.file;
    }
}
