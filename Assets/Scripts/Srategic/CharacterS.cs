using Assets.Scripts;
using Assets.Scripts.Interchange;
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

    public TacticalItem RightHandItem => rightHandItem;

    public PrefabsController prefabsController => _prefabsController;

    Inventory ICharacter.inventory
    {
        get
        {
            return inventory;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Stats = new EntityStats()
        {
            inStrength = 4,
            inDexterity = 5,
            inAgility = 5,
            inConstitution = 5,
            inIntellect = 8,
            inConcentration = 11,
            inPerception = 6
        };
        Stats.CurrentHealth = Stats.MaxHealth;
    }

    public float armor
    {
        get
        {
            return (inventory.HelmetArmor + inventory.ChestArmor + inventory.BootsArmor + inventory.GlovesArmor) / 4f;
        }
    }

    public CharacterTransferData TransferData
    {
        get
        {
            return new CharacterTransferData
            {
                Stats = this.Stats,
                Inventory = this.inventory.TransferData,
                RightHand = this.RightHandItem is null ? null : this.RightHandItem.TransferData,
            };
        }
    }

    EntityStats ICharacter.Stats { get => Stats; set => Stats = value; }

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
            gameController.ShowMessage("������ ��������� ����������");
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
            var obj = ItemFactory.CreateItem(loot, gameController.GroundPanel, this);
            var item = obj.GetComponent<Item>();
            var itemRef = item.itemRef;

            gameController.AddItemToSector(gameController._currentSector, itemRef);

            gameController.ShowMessage("�� �����: " + item.Name, itemRef.image);
        }
        else
        {
            gameController.ShowMessage("�� ������ �� �����");
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
        rightHandItem = item.thing.GetComponent<TacticalItem>();
    }

    public void RemoveFromRightHand(bool drop)
    {
        rightHandItem = null;
    }
}
