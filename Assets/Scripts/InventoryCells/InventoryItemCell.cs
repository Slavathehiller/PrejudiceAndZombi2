using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemCell : EquipmentCell
{

    public GameObject container;
    
    public override void PlaceItemToCell(EquipmentItem thing)
    {
        base.PlaceItemToCell(thing);
        var _thing = (InventoryEquipmentItem)thing;
        if (_thing != null)
        {
            _thing.cellScheme.transform.SetParent(container.transform);
            _thing.cellScheme.transform.localPosition = Vector3.zero;
            _thing.cellScheme.SetActive(true);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
