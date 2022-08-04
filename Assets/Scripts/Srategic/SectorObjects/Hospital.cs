using UnityEngine;

public class Hospital : SectorObject
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
        prefab = prefabsController.hospital;
        loot = new Loot[3]
        {
            new Loot()
            {
                prefab = prefabsController.medicalBandage,
                chance = 20f
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
