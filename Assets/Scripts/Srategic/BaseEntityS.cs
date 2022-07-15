using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityS : MonoBehaviour
{
    public EntityStats Stats;

    public float MaxHealth
    {
        get
        {
            return Stats.MaxHealth;
        }
    }

    public float CurrentHealth
    {
        get { return Stats.CurrentHealth; }
        set { Stats.CurrentHealth = value; }
    }

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
    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
