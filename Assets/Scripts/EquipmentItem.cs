using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public GameObject cellScheme;
    public SpecType specType;
    void Start()
    {

    }


    public void TakeBackScheme()
    {
        cellScheme.transform.SetParent(gameObject.transform);
        cellScheme.SetActive(false);
    }

}
