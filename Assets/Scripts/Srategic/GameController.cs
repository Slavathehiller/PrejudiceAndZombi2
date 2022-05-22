using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject selector;
    public GameObject cameraContainer;
    [HideInInspector]
    public bool cameraMoving = false;
    public GameObject[] Panels;
    

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
