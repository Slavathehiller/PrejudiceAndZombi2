using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Pliers : Tool
{
    protected override void Awake()
    {
        base.Awake();
        Name = "�����������";
        prefab = prefabsController.pliers;
    }
}
