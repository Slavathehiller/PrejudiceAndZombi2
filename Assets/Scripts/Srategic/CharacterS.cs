using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterS : BaseEntityS, ICharacter
{
    
    public Inventory inventory;
    public Sack sack;
    public GameController gameController;
    public PrefabsController _prefabsController;
    public TacticalItem rightHandItem;

    Inventory ICharacter.inventory => inventory;

    public TacticalItem RightHandItem => rightHandItem;

    public PrefabsController prefabsController => _prefabsController;

    // Start is called before the first frame update
    void Start()
    {
        Stats = new EntityStats()
        {
            inStrength = 4,
            inDexterity = 6,
            inAgility = 8,
            inConstitution = 5,
            inIntellect = 8,
            inConcentration = 7,
            inPerception = 6
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.findResult)
        {
            findProcess();
            gameController.findResult = false;
        }
    }

    public void DoSearch()
    {
        if(gameController._currentSector.sectorObject.findChance <= 0)
        {
            gameController.ShowMessage("Сектор полностью разграблен");
            return;
        }
        if (!gameController.isLocked())        
            gameController.FindProcess();       
    }

    private void findProcess()
    {
        var loot = Find();
        if (loot != null)
        {
            var obj = Instantiate(loot, null);           
            var itemRef = obj.GetComponent<Item>().itemRef;
            itemRef.character = this;
            itemRef.transform.SetParent(gameController.GroundPanel.transform);
            itemRef.transform.localPosition = Vector3.zero;
            itemRef.gameObject.SetActive(true);
            gameController._currentSector.sectorObject.AddItem(itemRef);
            gameController.allItems.Add(itemRef);
            gameController.ShowMessage("Вы нашли: " + obj.GetComponent<Item>().Name, itemRef.image);
        }
        else
        {
            gameController.ShowMessage("Вы ничего не нашли");
        }
        var findChanceDecrease = Mathf.Max(20 - Observation, 1);
        gameController._currentSector.sectorObject.findChance -= Mathf.Round(Mathf.Clamp(findChanceDecrease, findChanceDecrease, gameController._currentSector.sectorObject.findChance));
        gameController.RefreshSectorData();
    }
    public GameObject Find()
    {
        float Find = Random.Range(0, 100);
        if (Find <= gameController.currentSector.sectorObject.findChance)
        {
            Find = Random.Range(0, gameController.currentSector.sectorObject.loot.Sum((x)=>x.chance));
            var f = 0f;
            for (var i = 0; i < gameController.currentSector.sectorObject.loot.Length; i++)
            {
                f += gameController.currentSector.sectorObject.loot[i].chance;
                if (f >= Find)
                {
                    return gameController.currentSector.sectorObject.loot[i].prefab;
                }
            }
        }
        return null;
    }

    public void RemoveFromNearObjects(ItemReference item, bool hideGameObject = false){}

    public void PickUpItem(ItemReference item)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveFromRightHand(bool drop)
    {
        rightHandItem = null;
    }
}
