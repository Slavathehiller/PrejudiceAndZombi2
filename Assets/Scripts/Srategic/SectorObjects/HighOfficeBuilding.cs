using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighOfficeBuilding : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        prefab = prefabsController.highOffice;
        loot = new Loot[8]
        {   
            new Loot()
            {
                prefab = prefabsController.ductTape,
                chance = 15f
            },
            new Loot()
            {
                prefab = prefabsController.kitchenKnife,
                chance = 5f
            },
                new Loot(){
                prefab = prefabsController.simpleShirt,
                chance = 3f
            },
                new Loot(){
                prefab = prefabsController.shortTube,
                chance = 10
            },
            new Loot(){
                prefab = prefabsController.sneakers,
                chance = 3f
            },
            new Loot(){
                prefab = prefabsController.leatherJacket,
                chance = 2f
            },
                new Loot(){
                prefab = prefabsController.metalScrap,
                chance = 100f
            },
                new Loot(){
                prefab = prefabsController.wood,
                chance = 100f
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
