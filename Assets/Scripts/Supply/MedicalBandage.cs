using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalBandage : BaseSupply
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Медицинский бинт";
    }
}
