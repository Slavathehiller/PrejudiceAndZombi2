using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Reservoir
{
    protected override void Awake()
    {
        base.Awake();
        Name = "������ �������";
        Description = "������ ����������� ������� ������� ���� ����.";
    }
}
