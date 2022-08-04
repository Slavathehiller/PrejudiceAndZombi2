using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DuctTape : Item
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Изолента";
        prefab = prefabsController.ductTape;
    }
}
