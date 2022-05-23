using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighOfficeBuilding : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        loot = new Loot[2]
        {   
            new Loot()
            {
                prefab = prefabsController.kitchenKnife,
                chance = 50f
            },
            new Loot()
            {
                prefab = prefabsController.homemade_pistol,
                chance = 5f
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
