using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeScale
{
    Pause = 0,
    Normal = 1,
    Fast =  60
} 
public class Ticker : MonoBehaviour
{
    
    void Start()
    {   
        SetTimeScale(TimeScale.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public delegate void TickEvent();

    public event TickEvent Tick;

    private IEnumerator Ticking(TimeScale timing)
    {
        do
        {
            yield return new WaitForSeconds(1 / (float)timing);
            Tick?.Invoke();
        }
        while (true);
    }

    public void SetTimeScale(TimeScale timing)
    {
        StopAllCoroutines();
        if (timing != TimeScale.Pause)
        {  
            StartCoroutine(Ticking(timing));
        }
    }
}
