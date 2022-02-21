using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFittter : MonoBehaviour
{

    public RectTransform container;
    public RectTransform text;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var size = container.sizeDelta;
        size.y = text.sizeDelta.y + 10;
        container.sizeDelta = size;
    }
}
