using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public InterfaceController icontroller;

    public void OnPointerEnter(PointerEventData eventData)
    {
        icontroller.UIInact = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        icontroller.UIInact = false;
    }
}
