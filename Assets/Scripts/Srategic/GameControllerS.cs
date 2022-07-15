using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerS : MonoBehaviour
{
    public CharacterS character;
    public GameObject selector;
    public GameObject cameraContainer;
    public Text sectorFindChance;
    [HideInInspector]
    public bool cameraMoving = false;
    public GameObject[] Panels;
    public GameObject GroundPanel;
    public Slider findSlider;
    public List<ItemReference> SectorItems = new List<ItemReference>();
    public MessageWindowS messageWindowS;
    public GameObject playerSack;
    public GameObject sectorSack;
    public GameObject inventoryPanel;
    public GameObject sectorPanel;
    public GameObject inventoryContainer;
    public GameObject sectorContainer;
    public Text sectorObjectName;

    public Text armorText;

    public bool findResult = false;
    bool isFinding = false;
    public bool isMessaging = false;

    [HideInInspector]
    public bool UIInact = false;

    public Sector _currentSector;
    public Sector currentSector
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

    public void RefreshArmorText()
    {
        armorText.text = "Броня: " + character.armor.ToString() + "%";
    }

    public void RefreshSectorData()
    {
        sectorFindChance.text = _currentSector.sectorObject.findChance.ToString() + " %";
        findSlider.value = 0;
        foreach(var itemRef in SectorItems)
        {
            itemRef.gameObject.SetActive(_currentSector.sectorObject.sack.Contains(itemRef));
        }
        sectorObjectName.text = _currentSector.sectorObject.Name;
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
        RefreshSectorData();
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
        Global.stats = character.Stats;
        Global.inventory = character.inventory;
        Global.character = character.gameObject;
        Global.needToLoad = true;
        SceneManager.LoadScene(1);
    }

}
