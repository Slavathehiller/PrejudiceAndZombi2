using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeatherJacket : ArmorItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "������� ������";
        prefab = prefabsController.leatherJacket;
    }
}
