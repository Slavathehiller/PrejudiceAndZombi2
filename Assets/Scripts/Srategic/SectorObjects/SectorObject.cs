using Assets.Scripts.Interchange;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Loot
{
    public GameObject prefab;
    public float chance;
}

public abstract class SectorObject : MonoBehaviour
{
    public Loot[] loot;
    public GameObject[] mandatoryLoot;
    public List<ItemReference> sack = new List<ItemReference>();
    public string Name;
    public GameObject prefab;

    public float findChance = 100f;

    public PrefabsController prefabsController;

    public SectorObjectTransferData TransferData
    {
        get
        {
            return new SectorObjectTransferData
            {
                findChance = this.findChance,
                prefab = this.prefab,
                sack = new List<ItemTransferData>(this.sack.Select(x => x.Item.TransferData))
            };
        }
    }

    public void AddItem(ItemReference itemRef)
    {
        if(!sack.Contains(itemRef))
            sack.Add(itemRef);
    }

    public void RemoveItem(ItemReference itemRef)
    {
        sack.Remove(itemRef);
    }
}
