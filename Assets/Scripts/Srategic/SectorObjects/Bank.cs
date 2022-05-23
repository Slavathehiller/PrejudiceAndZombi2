using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        loot = new Loot[2]
        {
            new Loot()
            {
                prefab = prefabsController.Bullet_9x18,
                chance = 10f
            },
            new Loot(){
                prefab = prefabsController.medicalBandage,
                chance = 20f
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
