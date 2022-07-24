using Assets.Scripts;
using Assets.Scripts.Interchange;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
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
    
    public void AddItemToPlayerSack(ItemReference item)
    {
        character.sack.AddItem(item);
        item.transform.SetParent(BagagePanel.transform);
        item.transform.localScale = new Vector3(1, 1, 1);
        item.oldParent = null;
        item.canvasGroup.blocksRaycasts = true;
        item.background.enabled = true;
        item.character = character;
        item.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
        item.gameObject.SetActive(true);
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
            AddItemToSector(sector, itemData.Prefab, itemData.Count);
        }
    }

    public void RemoveFromCurrentSector(ItemReference item)
    {
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

    // Start is called before the first frame update
    void Start()
    {
        if (Global.needToLoad)
        {
            Global.ReloadCharacter(character);
            _lootInfoWindow.ShowLoot(Global.Loot);
            AddItemsToSector(CurrentSector, Global.Loot);
            Global.needToLoad = false;
        }
        RefreshSectorData();
        foreach(var panel in Panels)
        {
            if (panel.activeSelf)
                UIInact = true;
        }
    }

    // Update is called once per frame
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
        Global.character = character.TransferData;
        Global.needToLoad = true;
        SceneManager.LoadScene("TacticScene");
    }

}
