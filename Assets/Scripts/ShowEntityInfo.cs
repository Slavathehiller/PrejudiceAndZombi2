using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEntityInfo : MonoBehaviour
{

    public Text Name;
    public Image Portrait;
    public Text Health;
    public Text AP;
    public Text APSurplus;
    public Text Description;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInfo(BaseEntity entity)
    {
        gameObject.SetActive(true);
        Name.text = entity.Name;
        Portrait.sprite = entity.Portrait;
        Health.text = System.Math.Round(entity.CurrentHealth, 1).ToString() + '/' + System.Math.Round(entity.MaxHealth, 1).ToString();
        AP.text = System.Math.Round(entity.currentActionPoint, 1).ToString() + '/' + System.Math.Round(entity.MaxActionPoint, 1).ToString();
        APSurplus.text = System.Math.Round(entity.IncomeActionPoint, 1).ToString() + " за ход";
        Description.text = entity.Description;
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }

}
