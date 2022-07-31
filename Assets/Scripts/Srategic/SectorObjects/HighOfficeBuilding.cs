using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighOfficeBuilding : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        prefab = prefabsController.highOffice;
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
                chance = 30f
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
                prefab = prefabsController.sneakers,
                chance = 30000f
            },
            new Loot(){
                prefab = prefabsController.leatherJacket,
                chance = 30000f
            },
            new Loot(){
                prefab = prefabsController.constructionHelmet,
                chance = 30000f
            },
            new Loot(){
                prefab = prefabsController.armWindings,
                chance = 30000f
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
