using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        loot = new Loot[9]
        {
            new Loot()
            {
                prefab = prefabsController.Bullet_9x18,
                chance = 10f
            },
            new Loot(){
                prefab = prefabsController.medicalBandage,
                chance = 70f
            },
            new Loot(){
                prefab = prefabsController.ductTape,
                chance = 30f
            },
            new Loot(){
                prefab = prefabsController.shortTube,
                chance = 30f
            },
            new Loot(){
                prefab = prefabsController.longTube,
                chance = 20f
            },
            new Loot(){
                prefab = prefabsController.simpleBelt,
                chance = 20000f
            },
            new Loot(){
                prefab = prefabsController.simpleShirt,
                chance = 30f
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
