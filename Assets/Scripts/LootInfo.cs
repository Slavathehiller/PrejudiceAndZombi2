using Assets.Scripts;
using Assets.Scripts.Interchange;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject _lootPanel;
    private List<Item> _loot = new List<Item>();
    public void ShowLoot(IEnumerable<ItemTransferData> loot)
    {
        foreach(var item in loot)
        {
            var lootItem = ItemFactory.CreateItem(item.Prefab, _lootPanel).GetComponent<Item>();
            lootItem.SetCount(item.Count);
            _loot.Add(lootItem);
        }
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        foreach(var item in _loot)
        {
            Destroy(item.itemRef.gameObject);
            Destroy(item.gameObject);
        }
        gameObject.SetActive(false);
    }

}
