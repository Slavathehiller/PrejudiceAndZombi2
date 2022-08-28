using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterStateIndicator : MonoBehaviour
{

    [SerializeField] private CharacterS _character;
    [SerializeField] private Image _health;
    [SerializeField] private Image _energy;
    [SerializeField] private Image _food;
    [SerializeField] private Image _water;
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
        _food.fillAmount = _character.Food / 100;
        _water.fillAmount = _character.Water / 100;
    }
}
