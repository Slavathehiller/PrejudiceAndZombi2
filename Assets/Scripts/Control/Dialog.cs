using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Speach
{
    public ISpeakable actor;
    public string phrase;
}

public class Dialog : MonoBehaviour
{
    [SerializeField] private Image _portrait;
    [SerializeField] private Text _name;
    [SerializeField] private Text _speach;

    private List<Speach> _speaches;
    private int _currentSpeach = -1;
    public void SayNext()
    {
        if (_currentSpeach < 0)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            return;
        }
        _portrait.sprite = _speaches[_currentSpeach].actor.GetPortrait();
        _speach.text = _speaches[_currentSpeach].phrase;
        _currentSpeach++;
        if (_currentSpeach >= _speaches.Count - 1)
        {        
            _currentSpeach = -1 ;
        }
    }

    public void Say(Speach speach)
    {
        Say(new List<Speach>() { speach });
    }

    public void Say(List<Speach> speaches)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        _speaches = speaches;
        _currentSpeach = 0;
        SayNext();
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