using Assets.Scripts;
using Assets.Scripts.Entity;
using Assets.Scripts.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScenarioController : BaseScenarioController
{
    [SerializeField] CharacterT _character;
    [SerializeField] FreshZombi _zombiGirl;
    [SerializeField] KitchenKnife _knife;
    [SerializeField] Transform _mirror;


    private void Awake()
    {
        _lifeController.gameObject.SetActive(false);
        var item = ItemFactory.CreateItem(prefabsController.simpleJeans, null, _character).GetComponent<EquipmentItem>();
        _character.inventory.EquipItem(item, SpecType.EqPants);
        item = ItemFactory.CreateItem(prefabsController.simpleShirt, null, _character).GetComponent<EquipmentItem>();
        _character.inventory.EquipItem(item, SpecType.EqShirt);
        item = ItemFactory.CreateItem(prefabsController.simpleBelt, null, _character).GetComponent<EquipmentItem>();
        _character.inventory.EquipItem(item, SpecType.EqBelt);
    }

    private void Start()
    {
        _character.econtroller.animator.SetBool("StartAsLying", true);
        Phase1();
    }

    private void Phase1()
    {
        _dialog.Say(new Speach() { actor = _character, phrase = "Где я? Что случилось?" }, Phase2);

    }
    private void Phase2()
    {
        WaitAndGo
        (5,
        () => _zombiGirl.econtroller.MoveIfPossible(new Vector3(-3f, 0, -1), false)
        );
        _zombiGirl.econtroller.OnEndMoving += Phase3;        
    }

    private void Phase3()
    {
        _zombiGirl.econtroller.OnEndMoving -= Phase3;
        //_character.transform.LookAt(_zombiGirl.transform);
        _character.econtroller.TurnToObject( _zombiGirl );
        _dialog.Say(new List<Speach>(){
            new Speach() { actor = _character, phrase = "Что за..." },
            new Speach() { actor = _zombiGirl, phrase = "[Издает нечленораздельные звуки]" }
        }, Phase4);
    }

    private void Phase4()
    {
        _interfaceController.AllEnemyDead += Phase5;
        _lifeController.gameObject.SetActive(true);
        _character.isActive = false;
        _knife.boxCollider.enabled = true;
    }

    private void Phase5()
    {
        _interfaceController.AllEnemyDead -= Phase5;       
        _lifeController.gameObject.SetActive(false);
        _cameraContainer.transform.SetParent(_character.transform);
        WaitAndGo
        (2,
        () => _character.DropRightItem(true)
        );

        WaitAndGo
        (2,
        () =>
        {
            _interfaceController.HideActionPanel();
            _interfaceController.UIBlockedByScenario = true;
            _interfaceController.inventoryPanel.SetActive(false);
        }
        );

        WaitAndGo
        (4,
        () => _character.econtroller.MoveIfPossible(new Vector3(4f, 0, 1f), false)
        );
        _character.econtroller.OnEndMoving += Phase6;
    }

    private void Phase6()
    {
        _character.econtroller.OnEndMoving -= Phase6;
        _cameraContainer.transform.SetParent(null);
        WaitAndGo
        (1,
        () => _character.econtroller.TurnToObject(_mirror) //_character.econtroller.TurnLeft()//_character.transform.LookAt(_mirror)
        ) ;
    }
}
