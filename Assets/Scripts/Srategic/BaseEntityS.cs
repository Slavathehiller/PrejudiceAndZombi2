using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityS : MonoBehaviour
{
    protected EntityStats Stats;



    public float Stealth
    {
        get
        {
            return (Stats.Dexterity + Stats.Perceprion + Stats.Intellect) / 3;
        }
    }

    public float Observation
    {

        get
        {
            return Stats.Perceprion + Stats.Intellect / 3;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
