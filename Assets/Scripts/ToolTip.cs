using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject toolTipPanel;
    private Text toolTipText;
    [SerializeField]
    private string toolTip;
    private float canvasScale = 1;

    private void Start()
    {
        toolTipPanel = GameObject.Find("ToolTipContainer").transform.GetChild(0).gameObject;
        toolTipText = toolTipPanel.GetComponentInChildren<Text>();
        var canvas = GameObject.Find("UI_Canvas").GetComponent<Canvas>();
        if (canvas != null)
            canvasScale = canvas.scaleFactor;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ShowToolTip());   
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        toolTipPanel.SetActive(false);
    }

    public IEnumerator ShowToolTip()
    {
        yield return new WaitForSeconds(0.5f);
        var position = Input.mousePosition + new Vector3(0, 23, 0);
        var exceedScreenSize = position.x + toolTipPanel.GetComponent<RectTransform>().sizeDelta.x * canvasScale - Screen.width;
        if (exceedScreenSize > 0)
        {
            position.x = position.x - exceedScreenSize;
        }

        toolTipPanel.transform.position = position;

        toolTipText.text = toolTip;
        toolTipPanel.SetActive(true);
    }
}
