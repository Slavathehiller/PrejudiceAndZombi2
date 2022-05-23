using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Loot
{
    public GameObject prefab;
    public float chance;
}

public abstract class SectorObject : MonoBehaviour
{
    public Loot[] loot;
    public List<Item> sack = new List<Item>();
    public GameController gameController;

    public float findChance = 100f;

    public PrefabsController prefabsController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
