using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighOfficeBuilding : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        loot = new Loot[10]
        {   
            new Loot()
            {
                prefab = prefabsController.kitchenKnife,
                chance = 3000f
            },
            new Loot()
            {
                prefab = prefabsController.homemade_pistol,
                chance = 3000f
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
                prefab = prefabsController.simpleJeans,
                chance = 3000f
            },
            new Loot(){
                prefab = prefabsController.constructionHelmet,
                chance = 3f
            },
            new Loot(){
                prefab = prefabsController.armWindings,
                chance = 3f
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
