using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Ticking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public delegate void TickEvent();

    public event TickEvent Tick;

    IEnumerator Ticking()
    {
        do
        {
            yield return new WaitForSeconds(1);
            Tick?.Invoke();
        }
        while (true);
    }
}
