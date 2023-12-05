using Assets.Scripts.Interchange;
using Assets.Scripts.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    [SerializeField]private bool IsScripting;
    public PointerController movePointer;
    public PlayerController playerController;
    public GameObject inventoryPanel;
    public GameObject logPanel;
    public Text APText;
    public Text APSurplusText;
    public Text ArmorText;
    public Text Name;
    public Image Portrait;
    public Image HealthBarImage;
    public GameObject actionPanel;
    public GameObject[] commandButtons;
    public Text targetName;

    public Button rightHandDropButton;
    public Button leftHandDropButton;

    public GameObject rightHandPanel;

    public Material AccessMoveMaterial;
    public Material RestrictMoveMaterial;
    public event Action AllEnemyDead;

    [HideInInspector]
    public bool UIInact { get; set; } = false;
    [HideInInspector]
    public bool UIBlockedByScenario { get; set; } = false;

    [SerializeField]
    private EndBattleInfo _endBattleInfo;
    public EndBattleInfo EndBattleInfo => _endBattleInfo;

    // Start is called before the first frame update
    void Start()
    {
      //  inventoryPanel.SetActive(false);
        actionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        APText.text = System.Math.Round(playerController.character.currentActionPoint, 1).ToString();
        APSurplusText.text = System.Math.Round(playerController.character.IncomeActionPoint, 1).ToString();
        HealthBarImage.fillAmount = playerController.character.CurrentHealth / playerController.character.MaxHealth;
        Name.text = playerController.character.Name;
        //ArmorText.text = "Броня " + playerController.character.armor.ToString() + " %";
        Portrait.sprite = playerController.character.Portrait;
        UpdateActionPanel();
        rightHandDropButton.gameObject.SetActive(playerController.character.RightHandItem != null);
        leftHandDropButton.gameObject.SetActive(false);
    }

    private void UpdateActionPanel()
    {
        if (!actionPanel.activeSelf || playerController.selectedObject == null)
            return;

        var commands = playerController.selectedObject.getCommands();

        var distanceToObject = playerController.DistanceTo(playerController.selectedObject);
        commandButtons[(int)InteractableCommand.Punch].SetActive(commands.GetValue((int)InteractableCommand.Punch) != null && distanceToObject < 2 && playerController.character.RightHandItem == null);
        commandButtons[(int)InteractableCommand.Kick].SetActive(commands.GetValue((int)InteractableCommand.Kick) != null && distanceToObject < 2);
        commandButtons[(int)InteractableCommand.Stab].SetActive(commands.GetValue((int)InteractableCommand.Stab) != null && distanceToObject < 2 && playerController.character.RightHandItem is BaseWeapon && ((BaseWeapon)playerController.character.RightHandItem)?.type == WeaponType.Knife);
        commandButtons[(int)InteractableCommand.Shot].SetActive(commands.GetValue((int)InteractableCommand.Stab) != null && playerController.character.RightHandItem is BaseWeapon && CanShoot(((BaseWeapon)playerController.character.RightHandItem)?.type));
    }

    private bool CanShoot(WeaponType? type)
    {
        var allowedWeapon = new List<WeaponType?> { WeaponType.SMG, WeaponType.Pistol };
        return allowedWeapon.Contains(type);    
    }

    void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        var pointerPosition = hit.point;
        var movePosition = playerController.allignetPointMid(pointerPosition);
        movePointer.SetActive(playerController.PlayerCanMove && !UIInact && !UIBlockedByScenario);
        if (movePointer.activeSelf && movePointer.position != movePosition)
        {
            movePointer.position = movePosition;
            if (playerController.PlayerCanReach(movePointer.transform.position))
                movePointer.SetPointerMaterial(AccessMoveMaterial);
            else
                movePointer.SetPointerMaterial(RestrictMoveMaterial);
        }
    }

    void OnMouseExit()
    {
        movePointer.SetActive(false);
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
        targetName.text = obj.GetName();
    }

    public void HideActionPanel()
    {
        actionPanel.SetActive(false);
    }

    public void OnAllEnemyDead()
    {
        AllEnemyDead?.Invoke();
        if (!IsScripting)
            DoWinBattle();
    }

    public void DoWinBattle()
    {
        EndBattleInfo.gameObject.SetActive(true);
    }

    public void GoToStrategy()
    {
        Global.character = playerController.character.TransferData;
        Global.Loot = new List<ItemTransferData>(FindObjectsOfType<TacticalItem>().Where(x => x != playerController.character.RightHandItem).Select(x => x.TransferData));
        Global.lastStateOnLoad = StateOnLoad.LoadFromTactic ;

       SceneManager.LoadScene(Global.locationTransferData.SceneName);
    }
}
