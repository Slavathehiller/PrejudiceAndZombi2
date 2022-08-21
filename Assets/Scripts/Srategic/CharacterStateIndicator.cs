using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterStateIndicator : MonoBehaviour
{

    [SerializeField] private CharacterS _character;
    [SerializeField] private Image _health;
    [SerializeField] private Image _energy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RefreshIndicators()
    {
        _health.fillAmount = _character.CurrentHealth / _character.MaxHealth;
        _energy.fillAmount = _character.CurrentEnergy / _character.MaxEnergy;
    }
}
