using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighOfficeBuilding : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        loot = new Loot[3]
        {   
            new Loot()
            {
                prefab = prefabsController.kitchenKnife,
                chance = 10f
            },
            new Loot()
            {
                prefab = prefabsController.homemade_pistol,
                chance = 1f
            },
                new Loot(){
                prefab = prefabsController.metalScrap,
                chance = 100f
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
