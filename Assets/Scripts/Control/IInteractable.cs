using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType { Undefined = -1, Enemy = 0, Ally = 1 }
public enum InteractableCommand { Punch = 0, Kick = 1, Stab = 2, Shot = 3}
public interface IInteractable
{
    InteractableType getType();

    string GetName();
    InteractableCommand[] getCommands();
    Vector3 GetPosition();

}
