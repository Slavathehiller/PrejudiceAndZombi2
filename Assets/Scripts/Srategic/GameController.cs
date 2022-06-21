using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject selector;
    public GameObject cameraContainer;
    public Text sectorFindChance;
    [HideInInspector]
    public bool cameraMoving = false;
    public GameObject[] Panels;
    public GameObject GroundPanel;
    public Slider findSlider;
    public List<ItemReference> allItems = new List<ItemReference>();
    public MessageWindowS messageWindowS;

    public bool findResult = false;
    bool isFinding = false;
    public bool isMessaging = false;


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

    public void RefreshSectorData()
    {
        sectorFindChance.text = _currentSector.sectorObject.findChance.ToString() + " %";
        findSlider.value = 0;
        foreach(var itemRef in allItems)
        {
            itemRef.gameObject.SetActive(_currentSector.sectorObject.sack.Contains(itemRef) || ((CharacterS)itemRef.character).sack.Contains(itemRef));
        }
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
        for(var i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(i == index);
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
}
