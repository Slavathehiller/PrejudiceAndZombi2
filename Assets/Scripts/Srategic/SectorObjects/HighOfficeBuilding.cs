using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighOfficeBuilding : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        loot = new Loot[7]
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
                prefab = prefabsController.ductTape,
                chance = 30f
            },
                new Loot(){
                prefab = prefabsController.shortTube,
                chance = 20f
            },
            new Loot(){
                prefab = prefabsController.longTube,
                chance = 10f
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
