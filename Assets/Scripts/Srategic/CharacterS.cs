using Assets.Scripts;
using Assets.Scripts.Interchange;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterS : BaseEntityS, ICharacter 
{    
    public Inventory inventory;
    public Sack sack;
    public GameController gameController;
    public PrefabsController _prefabsController;
    private TacticalItem _rightHandItem;

    public TacticalItem RightHandItem => _rightHandItem;

    public PrefabsController prefabsController => _prefabsController;

    public float SearchEnergyCost
    {
        get
        {
            return 3f;
        }
    }

    public float BattleEnergyCost
    {
        get
        {
            return 10f;
        }
    }

    public float MaxHealth
    {
        get
        {
            return Stats.MaxHealth;
        }
    }

    public float MaxEnergy
    {
        get
        {
            return Stats.MaxEnergy;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return Stats.CurrentHealth;
        }
        set
        {
            Stats.CurrentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }
    }

    public float CurrentEnergy
    {
        get
        {
            return Stats.CurrentEnergy;
        }
        set
        {
            Stats.CurrentEnergy = Mathf.Clamp(value, 0, MaxEnergy);
        }
    }
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
            inStrength = 3,
            inDexterity = 10,
            inAgility = 10,
            inConstitution = 1,
            inIntellect = 8,
            inConcentration = 10,
            inPerception = 6
        };
        Stats.CurrentHealth = Stats.MaxHealth;
        Stats.CurrentEnergy = Stats.MaxEnergy;
        Food = 100;
        Water = 100;
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
                Sack = this.sack.TransferData
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
        if(gameController.CurrentSector.sectorObject.findChance <= 0)
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
            var obj = ItemFactory.CreateItem(loot, gameController.GroundPanel, this);
            var item = obj.GetComponent<Item>();
            var itemRef = item.itemRef;

            gameController.AddItemToSector(gameController.CurrentSector, itemRef);

            gameController.ShowMessage("Вы нашли: " + item.Name, itemRef.image);
        }
        else
        {
            gameController.ShowMessage("Вы ничего не нашли");
        }
        CurrentEnergy -= SearchEnergyCost;
        var findChanceDecrease = Mathf.Max(20 - Observation, 1);
        gameController.CurrentSector.sectorObject.findChance -= Mathf.Round(Mathf.Clamp(findChanceDecrease, findChanceDecrease, gameController.CurrentSector.sectorObject.findChance));
        gameController.RefreshSectorData();
    }
    public GameObject Find()
    {
        float Find = Random.Range(0, 100);
        if (Find <= gameController.CurrentSector.sectorObject.findChance)
        {
            Find = Random.Range(0, gameController.CurrentSector.sectorObject.loot.Sum((x)=>x.chance));
            var f = 0f;
            for (var i = 0; i < gameController.CurrentSector.sectorObject.loot.Length; i++)
            {
                f += gameController.CurrentSector.sectorObject.loot[i].chance;
                if (f >= Find)
                {
                    return gameController.CurrentSector.sectorObject.loot[i].prefab;
                }
            }
        }
        return null;
    }

    public void RefreshConditions()
    {
        if(CurrentHealth < MaxHealth)
            CurrentHealth += HealthRestoreRatio;
        if (CurrentEnergy < MaxEnergy)
            CurrentEnergy += EnergyRestoreRatio;
        Food -= HungerIncreaseRatio;
        Water -= ThirstIncreaseRatio;
    }

    public void RemoveFromNearObjects(ItemReference item, bool hideGameObject = false){}

    public void PickUpItem(ItemReference item)
    {
        _rightHandItem = item.thing.GetComponent<TacticalItem>();
    }

    public void RemoveFromRightHand(bool drop)
    {
        _rightHandItem = null;
    }
}
