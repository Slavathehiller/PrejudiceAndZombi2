using System.Collections;
using System.Collections.Generic;
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


    private Sector _currentSector;
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
        sectorFindChance.text = _currentSector.sectorObject.findChance.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelsButtonClick(int index)
    {        
        for(var i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(i == index);
        }
    }
}
