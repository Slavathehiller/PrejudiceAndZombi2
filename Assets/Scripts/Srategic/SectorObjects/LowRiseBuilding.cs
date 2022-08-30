using UnityEngine;

public class LowRiseBuilding : SectorObject
{
    private void Awake()
    {
        mandatoryLoot = new GameObject[2]
        {
            prefab = prefabsController.apple,
            prefab = prefabsController.bottleOfWater
        };
    }
    // Start is called before the first frame update
    void Start()
    {
        prefab = prefabsController.lowRiseBuilding;
        loot = new Loot[10]
        {
            new Loot()
            {
                prefab = prefabsController.kitchenKnife,
                chance = 10f
            },
            new Loot(){
                prefab = prefabsController.medicalBandage,
                chance = 20f
            },
            new Loot(){
                prefab = prefabsController.simpleBelt,
                chance = 3f
            },
                new Loot(){
                prefab = prefabsController.simpleShirt,
                chance = 3f
            },
                new Loot(){
                prefab = prefabsController.simpleJeans,
                chance = 3f
            },
            new Loot(){
                prefab = prefabsController.sneakers,
                chance = 3f
            },
            new Loot(){
                prefab = prefabsController.leatherJacket,
                chance = 3f
            },
            new Loot(){
                prefab = prefabsController.hammer,
                chance = 2f
            },
            new Loot(){
                prefab = prefabsController.apple,
                chance = 5f
            },
                new Loot(){
                prefab = prefabsController.ductTape,
                chance = 20f
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
