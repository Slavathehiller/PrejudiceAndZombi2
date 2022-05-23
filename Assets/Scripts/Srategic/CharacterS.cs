using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterS : MonoBehaviour
{
    private EntityStates Stats;
    public Inventory inventory;
    public Sack sack;
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoSearch()
    {
        var loot = Find();
        if (loot != null)
        {
            var obj = Instantiate(loot, null);
            var itemRef = obj.GetComponent<Item>().itemRef;
            itemRef.transform.SetParent(gameController.GroundPanel.transform);
            itemRef.transform.localPosition = Vector3.zero;
            itemRef.gameObject.SetActive(true);
        }
    }
    public GameObject Find()
    {
        var Find = Random.Range(0, gameController.currentSector.sectorObject.findChance);
        if (Find <= gameController.currentSector.sectorObject.findChance)
        {
            Find = Random.Range(0, gameController.currentSector.sectorObject.loot.Sum((x)=>x.chance));
            var f = 0f;
            for (var i = 0; i < gameController.currentSector.sectorObject.loot.Length; i++)
            {
                f += gameController.currentSector.sectorObject.loot[i].chance;
                if (f >= Find)
                {
                    return gameController.currentSector.sectorObject.loot[i].prefab;
                }
            }
        }
        return null;
    }

}
