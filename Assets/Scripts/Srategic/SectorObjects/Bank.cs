public class Bank : SectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        loot = new Loot[12]
        {
            new Loot()
            {
                prefab = prefabsController.Bullet_9x18,
                chance = 30000f
            },
            new Loot(){
                prefab = prefabsController.sneakers,
                chance = 7f
            },
            new Loot(){
                prefab = prefabsController.leatherJacket,
                chance = 7f
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
                chance = 400f
            },
            new Loot(){
                prefab = prefabsController.simpleShirt,
                chance = 40000f
            },
            new Loot(){
                prefab = prefabsController.constructionHelmet,
                chance = 3f
            },
            new Loot(){
                prefab = prefabsController.simpleJeans,
                chance = 400f
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
