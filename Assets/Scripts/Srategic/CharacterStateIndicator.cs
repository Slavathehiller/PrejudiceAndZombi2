using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterStateIndicator : MonoBehaviour
{

    [SerializeField] private CharacterS _character;
    [SerializeField] private Image _health;
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
    }
}
