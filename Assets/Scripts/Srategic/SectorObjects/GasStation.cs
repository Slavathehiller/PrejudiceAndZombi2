using UnityEngine;

public class GasStation : SectorObject
{
    private void Awake()
    {
        mandatoryLoot = new GameObject[0]
        {

        };
    }
    // Start is called before the first frame update
    void Start()
    {
        prefab = prefabsController.gasStation;
        loot = new Loot[6]
        {
            new Loot()
            {
                prefab = prefabsController.longTube,
                chance = 20f
            },
            new Loot(){
                prefab = prefabsController.shortTube,
                chance = 20f
            },
            new Loot(){
                prefab = prefabsController.ductTape,
                chance = 20f
            },
            new Loot(){
                prefab = prefabsController.leatherJacket,
                chance = 3f
            },
                new Loot(){
                prefab = prefabsController.simpleJeans,
                chance = 3f
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
