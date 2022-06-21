using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public Inventory inventory { get; }
    public void RemoveFromNearObjects(ItemReference item, bool hideGameObject = false);
    public TacticalItem RightHandItem { get; }
    public void PickUpItem(ItemReference item);
    public void RemoveFromRightHand(bool drop);
    public PrefabsController prefabsController { get; }

}
