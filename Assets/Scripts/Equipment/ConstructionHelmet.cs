using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionHelmet : ArmorItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Строительная каска";
    }
}
