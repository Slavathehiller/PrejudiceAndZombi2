using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : EntityController
{

    public Camera cam;
    public Character character;
    public IInteractable selectedObject;
    public NearObjects nearObjects;
    

    protected override void Start()
    {
        base.Start();
        character = GetComponent<Character>();
        nearObjects = GetComponent<NearObjects>();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            if (!(EventSystem.current?.IsPointerOverGameObject() == true && EventSystem.current.currentSelectedGameObject?.tag == "UI"))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && !icontroller.UIInact)
                {
                    var targetPoint = hit.point;
                    character.Move(targetPoint);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.SkipTurn();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            icontroller.HideActionPanel();
            selectedObject = null;
        }
    }

    public bool PlayerCanMove
    {
        get 
        { 
            return character.isActive && character.isMyTurn && !character.isActing; 
        }
    }

    public bool PlayerCanReach(Vector3 targetPoint)
    {
        NavMeshPath path = new NavMeshPath();
        return CalculateCompletePath(targetPoint, path) && PathReachable(path);
    }

    public void SelectObject(IInteractable obj)
    {
        if (obj.getType() == InteractableType.Undefined)
            return;
        if (!character.isActing)
        {
            selectedObject = obj;
            gameObject.transform.LookAt(obj.GetPosition());
            icontroller.ShowActionPanelForObject(obj);
        }
    }
    public void PickUpItem(ItemReference item)
    {
        character.TakeToRightHand(item.thing);
        nearObjects.DeleteThing(item);
    }

    public BaseEntity SelectedEnemy
    {
        get
        {
            var selectedEntity = selectedObject as IInteractableEntity;
            if (selectedEntity != null && selectedEntity.getType() == InteractableType.Enemy)
                return selectedEntity.GetEntity();
            else
                return null;
        }
    }

    

}
