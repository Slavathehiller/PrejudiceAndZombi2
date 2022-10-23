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


    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    public void OnChoose(int value) 
    {
        hoursAmount = value;
    }

    IEnumerator sleepProcess()
    {
        var finalDateTime = gameController.CurrentDateTime.AddHours(hoursAmount);
        gameController.character.isSleeping = true;
        gameController.SetTimeFast();
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
            gameController.SetTimeNormal();
            gameController.character.isSleeping = false;
        }  
    }

    public void Sleep() 
    {
        StartCoroutine(sleepProcess());
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
