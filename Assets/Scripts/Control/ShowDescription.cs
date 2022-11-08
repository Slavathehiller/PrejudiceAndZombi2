using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowDescription : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private CharacterInfoPanel characterInfo;
    [SerializeField] private BaseStats type;

    public void OnPointerEnter(PointerEventData eventData)
    {
        characterInfo.ShowDescription(type);
    }


}
