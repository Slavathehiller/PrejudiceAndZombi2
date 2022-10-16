using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InterfaceController icontroller;

    private void Start()
    {
        icontroller = FindObjectOfType<InterfaceController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(icontroller != null)
            icontroller.UIInact = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (icontroller != null)
            icontroller.UIInact = false;
    }
}
