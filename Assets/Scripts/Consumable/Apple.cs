using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Food
{
    protected override void Awake()
    {
        base.Awake();
        Name = "яблоко";
        prefab = prefabsController.apple;
        foodRestore = 5f;
        waterRestore = 3f;
    }
}
