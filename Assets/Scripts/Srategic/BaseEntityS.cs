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

    public float HealthRestoreRatio
    {
        get
        {
            return Stats.Constitution / 50 * (Food / 100);
        }
    }
    public float EnergyRestoreRatio
    {
        get
        {
            return Stats.Constitution / 40 * (Water / 100);
        }
    }

    public float HungerIncreaseRatio
    {
        get
        {
            return Stats.Constitution / 60;
        }
    }

    public float ThirstIncreaseRatio
    {
        get
        {
            return Stats.Constitution / 20;
        }
    }

    public float Food
    {
        get
        {
            return Stats.Food;
        }
        set
        {
            Stats.Food = Mathf.Clamp(value, 0, 100);
        }
    }

    public float Water
    {
        get
        {
            return Stats.Water;
        }
        set
        {
            Stats.Water = Mathf.Clamp(value, 0, 100);
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
