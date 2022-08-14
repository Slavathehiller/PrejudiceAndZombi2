using Assets.Scripts;
using Assets.Scripts.Interchange;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private string _templateName;
    [SerializeField] private GameObject _ground;
    public CharacterS character;
    public GameObject selector;
    public GameObject cameraContainer;
    public Text sectorFindChance;
    [HideInInspector]
    public bool cameraMoving = false;
    public GameObject[] Panels;
    public GameObject GroundPanel;
    public GameObject BagagePanel;
    public Slider findSlider;
    public List<ItemReference> SectorItems = new List<ItemReference>();
    public MessageWindowS messageWindowS;
    public GameObject playerSack;
    public GameObject sectorSack;
    public GameObject inventoryPanel;
    public GameObject sectorPanel;
    public GameObject inventoryContainer;
    public GameObject sectorContainer;

    private Ticker _ticker;

    public Text dateIndicator;
    private DateTime _currentDateTime;

    public Text armorText;
    public Text SectorObjectName;

    public bool findResult = false;
    bool isFinding = false;
    public bool isMessaging = false;

    [HideInInspector]
    public bool UIInact = false;

    [SerializeField]
    private Sector _currentSector;
    public Sector CurrentSector
    {
        get
        {
            return _currentSector;
        }
        set
        {
            _currentSector = value;
            RefreshSectorData();
        }
    }

    [SerializeField]
    private LootInfo _lootInfoWindow;

    public void RefreshSectorData()
    {
        sectorFindChance.text = CurrentSector.sectorObject.findChance.ToString() + " %";
        SectorObjectName.text = CurrentSector.sectorObject.Name;
        findSlider.value = 0;
        foreach(var itemRef in SectorItems)
        {
            itemRef.gameObject.SetActive(CurrentSector.sectorObject.sack.Contains(itemRef));
        }
    }
    
    public LocationTransferData TransferData
    {
        get
        {
            return new LocationTransferData()
            {
                SceneName = _templateName,
                sectors = new List<SectorTransferData>(FindObjectsOfType<Sector>().Select(x => x.TransferData)),
                currentSectorPosition = CurrentSector.transform.position
            };
        }
    }

    public void AddItemToPlayerSack(ItemReference item)
    {
        character.sack.AddItem(item);
        item.transform.SetParent(BagagePanel.transform);
        item.transform.localScale = Vector3.one;
        item.oldParent = null;
        item.canvasGroup.blocksRaycasts = true;
        item.background.enabled = true;
        item.character = character;
        item.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
        item.gameObject.SetActive(true);
        item.thing.gameObject.SetActive(false);
    }

    public void AddItemsToPlayerSack(IEnumerable<ItemTransferData> items)
    {
        foreach(var item in items)
        {
            AddItemToPlayerSack(item.Restore(BagagePanel, character).itemRef);
        }
    }

    public void RemoveFromPlayerSack(ItemReference item)
    {
        character.sack.RemoveItem(item);
    }

    public void AddItemToSector(Sector sector, ItemReference item)
    {
        sector.sectorObject.AddItem(item);
        SectorItems.Add(item);
    }

    public void AddItemToSector(Sector sector, GameObject prefab, int count = 1)
    {
        var obj = ItemFactory.CreateItem(prefab, GroundPanel, character);
        var item = obj.GetComponent<Item>();
        item.SetCount(count);
        AddItemToSector(sector, item.itemRef);        
    }

    public void AddItemsToSector(Sector sector, IEnumerable<ItemTransferData> data)
    {
        foreach ( var itemData in data)
        {
            AddItemToSector(sector, itemData.Restore(GroundPanel, character).itemRef);
        }
    }

    public void RemoveFromCurrentSector(ItemReference item)
    {
        if (CurrentSector != null)
            RemoveFromSector(CurrentSector, item);
    }

    public void RemoveFromSector(Sector sector, ItemReference item)
    {
        sector.sectorObject.RemoveItem(item);
        SectorItems.Remove(item);
    }

    public void FindProcess()
    {
        RefreshSectorData();
        findResult = false;
        isFinding = true;
    }


    private void LoadSectors()
    {
        foreach(var sector in Global.locationTransferData.sectors)
        {
            var sec = sector.Restore(this);
            if (sector.position == Global.locationTransferData.currentSectorPosition)
                sec.BecameCurrentInstant();
        }
    }

    public void RefreshDateTime()
    {
        _currentDateTime = _currentDateTime.AddMinutes(1);
        dateIndicator.text = _currentDateTime.ToString("dd.MM.yyyy HH:mm");
    }

    void Start()
    {
        if (Global.lastStateOnLoad == StateOnLoad.StartGame)
        {
            DateTime dateTime = new DateTime();
            dateTime = dateTime.AddYears(2047);
            dateTime = dateTime.AddMonths(3);
            _currentDateTime = dateTime;
        }
        if (Global.lastStateOnLoad == StateOnLoad.LoadFromTactic)
        {
            Global.ReloadCharacter(character);
            LoadSectors();
            AddItemsToPlayerSack(Global.character.Sack);

            if (Global.Loot != null)
            {
                _lootInfoWindow.ShowLoot(Global.Loot);
                AddItemsToSector(CurrentSector, Global.Loot);
                Global.Loot = null;
            }
        }       
        RefreshSectorData();
        foreach(var panel in Panels)
        {
            if (panel.activeSelf)
                UIInact = true;
        }
        _ticker = GetComponent<Ticker>();
        var indicator = GetComponent<CharacterStateIndicator>();
        indicator.RefreshIndicators();
        _ticker.Tick += RefreshDateTime;
        _ticker.Tick += character.RefreshConditions;
        _ticker.Tick += indicator.RefreshIndicators;        
    }

    void Update()
    {
        if (isFinding)
        {
            findSlider.value += Time.deltaTime;
            if (findSlider.value >= 1)
            {
                isFinding = false;
                findResult = true;
                RefreshSectorData();
            }
        }
    }

    public void PanelsButtonClick(int index)
    {
        if (isLocked())
            return;
        for(var i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(i == index);            
        }
        UIInact = index > -1;
        if (inventoryPanel.activeSelf)
        {
            playerSack.transform.SetParent(inventoryContainer.transform);
            sectorSack.transform.SetParent(inventoryContainer.transform);
        }
        if (sectorPanel.activeSelf)
        {
            playerSack.transform.SetParent(sectorContainer.transform);
            sectorSack.transform.SetParent(sectorContainer.transform);
        }

    }

    public bool isLocked() 
    {
        return isFinding || cameraMoving || isMessaging;
    }

    public void ShowMessage(string text, Image image = null)
    {
        messageWindowS.ShowMessage(text, image);
    }

    public void Engage()
    {
        if (isLocked())
            return;
        Global.locationTransferData = TransferData;
        Global.character = character.TransferData;
        Global.lastStateOnLoad = StateOnLoad.LoadFromStrategy;
        SceneManager.LoadScene("TacticScene");
    }

    public void SetPause(bool isChecked)
    {
        if(isChecked)
            _ticker.SetTimeScale(TimeScale.Pause);
    }
    public void SetNormal(bool isChecked)
    {
        if (isChecked)
            _ticker.SetTimeScale(TimeScale.Normal);
    }
    public void SetFast(bool isChecked)
    {
        if (isChecked)
            _ticker.SetTimeScale(TimeScale.Fast);
    }

}
