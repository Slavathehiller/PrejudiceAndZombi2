using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableEntity : IInteractable
{
    BaseEntity GetEntity();
}
