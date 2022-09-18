using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct MenuPointData
{
    public string Caption { get; set; }
    public UnityAction Command { get; set; }
    public Predicate<ItemReference> IsEnabled { get; set; }
}

public class PopupController : MonoBehaviour, IPointerExitHandler
{
    private GameObject parentItem;
    public GameObject ParentItem
    {
        get { return parentItem; }
        set { parentItem = value; }
    }
    
    public event Action OnPopup;
    public static MenuPointData CreateMenuPointData(string caption, UnityAction command, Predicate<ItemReference> isEnabled = null)
    {
        return new MenuPointData { Caption = caption, Command = command, IsEnabled = isEnabled };
    }

    public void Popup(GameObject parent)
    {
        transform.SetParent(parent.transform);
        gameObject.SetActive(true);
        OnPopup?.Invoke();
    }

    public void Init(ItemReference parentItem, MenuPointData[] menuCommands)
    {
        
        ParentItem = parentItem.gameObject;
        transform.localPosition = new Vector3(0, -10, 0);
        foreach(MenuPointData data in menuCommands)
        {
            AddMenuPoint(data, parentItem);
        }
        gameObject.SetActive(false);
    }

    public void AddMenuPoint(MenuPointData data, ItemReference owner)
    {
        var prefabsController = GameObject.Find("PrefabsController").GetComponent<PrefabsController>();
        var menuPointObj = Instantiate(prefabsController.menuPoint, gameObject.transform);
        var button = menuPointObj.GetComponent<Button>();
        button.onClick.AddListener(data.Command);
        button.onClick.AddListener(() => Hide());
        var menuPoint = menuPointObj.GetComponent<MenuPoint>();
        menuPoint.Caption.text = data.Caption;
        menuPoint.ItemRef = owner;
        if (data.IsEnabled is null)
            menuPoint.CheckIfEnable = (x) => true;
        else
            menuPoint.CheckIfEnable = data.IsEnabled;
        OnPopup += menuPoint.SetEnable;
    }

    private void Hide()
    {
        transform.SetParent(ParentItem.transform);
        transform.localPosition = new Vector3(0, -10, 0);
        gameObject.SetActive(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
        eventData.Reset();
    }
}
