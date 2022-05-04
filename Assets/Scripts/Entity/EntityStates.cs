using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStates
{
    public float inStrength = 0;
    public float inDexterity = 0;
    public float inAgility = 0;
    public float inConstitution = 0;
    public float inIntellect = 0;
    public float inConcentration = 0;
    public float inPerception = 0;

    public float MaxHealth
    {
        get
        {
            var result = Constitution * 10;

            return result;
        }
    }

    public float CurrentHealth { get; set; }

    public float Strength
    {
        get
        {
            var result = inStrength;

            return result;
        }
    }

    public float Dexterity
    {
        get
        {
            var result = inDexterity;

            return result;
        }
    }

    public float Agility
    {
        get
        {
            var result = inAgility;

            return result;
        }
    }

    public float Constitution
    {
        get
        {
            var result = inConstitution;

            return result;
        }
    }

    public float Intellect
    {
        get
        {
            var result = inIntellect;

            return result;
        }
    }
    public float Concentration
    {
        get
        {
            var result = inConcentration;

            return result;
        }
    }

    public float Perceprion
    {
        get
        {
            var result = inPerception;

            return result;
        }
    }
}
