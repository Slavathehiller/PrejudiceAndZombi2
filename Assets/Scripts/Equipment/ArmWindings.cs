using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmWindings : ArmorItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Обмотки рук";
        prefab = prefabsController.armWindings;
    }
}
