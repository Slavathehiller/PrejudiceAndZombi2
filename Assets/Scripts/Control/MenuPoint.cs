using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPoint : MonoBehaviour
{
    [SerializeField]private Text caption;
    public Text Caption => caption;
    
    private Item item;
    public Item Item 
    { 
        get 
        { 
            return item; 
        } 
        set
        {
            item = value;
        }
    }

    public Predicate<Item> CheckIfEnable;
    public void SetEnable()
    {
        gameObject.SetActive(false);
        GetComponent<Button>().interactable = CheckIfEnable(item);
        gameObject.SetActive(true);
    }
}