using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindowS : MonoBehaviour
{
    public Image MessageImage;
    public Text MessageText;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMessage(string text, Image image = null)
    {
        MessageText.text = text;
        MessageImage.sprite = image.sprite;
        MessageImage.GetComponent<RectTransform>().sizeDelta = image.GetComponent<RectTransform>().sizeDelta * 3;
        gameObject.SetActive(true);
        gameController.isMessaging = true;
    }

    public void OkClick()
    {
        gameObject.SetActive(false);
        gameController.isMessaging = false;
    }
}
