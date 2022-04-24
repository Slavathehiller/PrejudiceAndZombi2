using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    None = 0,
    FMG_9x18 = 1
}
public class Ammo : SMO
{
    public AmmoType type;
}
