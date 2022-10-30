using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScenarioController : BaseScenarioController
{
    [SerializeField] CharacterT _character;
    private void Start()
    {
        _dialog.Say(new Speach() { actor = _character, phrase = "Где я? Что случилось?" });
    }
}
