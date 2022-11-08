using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepControl : MonoBehaviour
{
    private int hoursAmount = 1;
    public GameController gameController;
    public Button sleepButton;
    public Button cancelButton;
    public Toggle OneHourToggle;
    public Toggle TwoHoursToggle;
    public Toggle FourHourToggle;
    public Toggle EightHourToggle;


    private GameController GameController
    {
        get
        {
            if (gameController is null)
                gameController = GameObject.Find("GameController").GetComponent<GameController>();
            return gameController;
        }
    }

    private void Start()
    {
        
    }
    public void OnChoose(int value) 
    {
        hoursAmount = value;
    }

    IEnumerator sleepProcess()
    {
        var finalDateTime = gameController.CurrentDateTime.AddHours(hoursAmount);
        GameController.character.isSleeping = true;
        GameController.SetTimeFast();
        sleepButton.interactable = false;
        cancelButton.interactable = false;
        OneHourToggle.interactable = false;
        TwoHoursToggle.interactable = false;
        FourHourToggle.interactable = false;
        EightHourToggle.interactable = false;
        try
        {
            while (gameController.CurrentDateTime < finalDateTime)
            {
                yield return null;
            }
        }
        finally 
        {
            sleepButton.interactable = true;
            cancelButton.interactable = true;
            OneHourToggle.interactable = true;
            TwoHoursToggle.interactable = true;
            FourHourToggle.interactable = true;
            EightHourToggle.interactable = true;
            GameController.SetTimeNormal();
            GameController.character.isSleeping = false;
            GameController.UIInact = true;
        }  
    }

    public void Sleep() 
    {
        StartCoroutine(sleepProcess());
    }

    public void Cancel()
    {
        GameController.UIInact = true;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        GameController.UIInact = true;
        gameObject.SetActive(true);
    }
}
