using UnityEngine;

public class PoliceStation : SectorObject
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
        prefab = prefabsController.policeStation;
        loot = new Loot[3]
        {
            new Loot()
            {
                prefab = prefabsController.Bullet_9x18,
                chance = 20f
            },
                new Loot(){
                prefab = prefabsController.medicalBandage,
                chance = 20f
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
