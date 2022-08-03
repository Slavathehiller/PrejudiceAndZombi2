using UnityEngine;

public class Bank : SectorObject
{
    private void Awake()
    {
        mandatoryLoot = new GameObject[15]
        {
            prefabsController.Bullet_9x18,
            prefabsController.simpleBelt,
            prefabsController.homemade_pistol,
            prefabsController.homemade_SMG,
            prefabsController.homemade_SMG_magazine,
            prefabsController.medicalBandage,
            prefabsController.medicalBandage,
            prefabsController.medicalBandage,
            prefabsController.medicalBandage,
            prefabsController.simpleShirt,
            prefabsController.simpleJeans,
            prefabsController.constructionHelmet,
            prefabsController.leatherJacket,
            prefabsController.armWindings,
            prefabsController.sneakers
        };
    }
    // Start is called before the first frame update
    void Start()
    {
        prefab = prefabsController.bank;
        loot = new Loot[5]
        {
            new Loot(){
                prefab = prefabsController.Bullet_9x18,
                chance = 2f
            },
            new Loot(){
                prefab = prefabsController.ductTape,
                chance = 15f
            },
            new Loot(){
                prefab = prefabsController.constructionHelmet,
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
