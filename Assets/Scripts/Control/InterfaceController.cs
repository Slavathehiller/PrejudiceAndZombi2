using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{

    public PointerController movePonter;
    public PlayerController playerController;
    public GameObject inventoryPanel;
    public GameObject logPanel;
    public Text APText;
    public Text APSurplusText;
    public Text Name;
    public Image Portrait;
    public Image HealthBarImage;
    public GameObject actionPanel;
    public GameObject[] commandButtons;
    public Text targetName;

    public Material AccessMoveMaterial;
    public Material RestrictMoveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(false);
        actionPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        APText.text = System.Math.Round(playerController.character.currentActionPoint, 1).ToString();
        APSurplusText.text = System.Math.Round(playerController.character.IncomeActionPoint, 1).ToString();
        HealthBarImage.fillAmount = playerController.character.currentHitpoint / playerController.character.MaxHealth;
        Name.text = playerController.character.Name;
        Portrait.sprite = playerController.character.portrait;
        UpdateActionPanel();
    }

    private void UpdateActionPanel()
    {
        if (!actionPanel.activeSelf || playerController.selectedObject == null)
            return;

        var commands = playerController.selectedObject.getCommands();

        var distanceToObject = playerController.DistanceTo(playerController.selectedObject);
        commandButtons[(int)InteractableCommand.Punch].SetActive(commands.GetValue((int)InteractableCommand.Punch) != null && distanceToObject < 2);
        commandButtons[(int)InteractableCommand.Kick].SetActive(commands.GetValue((int)InteractableCommand.Punch) != null && distanceToObject < 2);


    }

    void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        var pointerPosition = hit.point;
        var movePosition = playerController.allignetPointMid(pointerPosition);
        movePonter.SetActive(playerController.PlayerCanMove);
        if (movePonter.activeSelf && movePonter.position != movePosition)
        {
            movePonter.position = movePosition;
            if (playerController.PlayerCanReach(movePonter.transform.position))
                movePonter.SetPointerMaterial(AccessMoveMaterial);
            else
                movePonter.SetPointerMaterial(RestrictMoveMaterial);

        }
    }

    void OnMouseExit()
    {
        movePonter.SetActive(false);
    }

    public void InventoryButtonClick()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
    public void LogButtonClick()
    {
        logPanel.SetActive(!logPanel.activeSelf);
    }

    public void EndTurnButtonClick()
    {
        playerController.character.SkipTurn();
    }
    public void ShowActionPanelForObject(IInteractable obj)
    {
        actionPanel.SetActive(true);
        targetName.text = obj.GetEntity().Name;

        //for(var i = 0; i < commandButtons.Length; i++)
        //{
        //    if(obj.getCommands().GetValue(i) != null)
        //    {
        //        commandButtons[i].SetActive(true);
        //    }
        //}
    }

    public void HideActionPanel()
    {
        actionPanel.SetActive(false);
    }

}
