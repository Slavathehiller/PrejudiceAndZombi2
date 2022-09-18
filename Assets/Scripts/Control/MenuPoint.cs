using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPoint : MonoBehaviour
{
    [SerializeField]private Text caption;
    public Text Caption => caption;
    
    private ItemReference itemRef;
    public ItemReference ItemRef 
    { 
        get 
        { 
            return itemRef; 
        } 
        set
        {
            itemRef = value;
        }
    }

    public Predicate<ItemReference> CheckIfEnable;
    public void SetEnable()
    {
        gameObject.SetActive(false);
        GetComponent<Button>().interactable = CheckIfEnable(itemRef);
        gameObject.SetActive(true);
    }
}
