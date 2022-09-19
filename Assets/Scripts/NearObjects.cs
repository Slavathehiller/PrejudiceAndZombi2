using Assets.Scripts.Entity;
using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NearObjects : MonoBehaviour
{
    public List<ItemReference> list = new List<ItemReference>();
    public PrefabsController pfController;
    public GameObject groundPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(TacticalItem item, ICharacter character)
    {
        var refItem = item.itemRef;
        refItem.character = character;
        refItem.gameObject.SetActive(true);
        refItem.transform.SetParent(groundPanel.transform);
        refItem.transform.localScale = new Vector3(1, 1, 1);
        refItem.background.enabled = true;       
        refItem.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize * 0.7f;
        refItem.canvasGroup.blocksRaycasts = true;
        //refItem.ShowUnloadButton(false);
        if (!list.Contains(refItem))
            list.Add(refItem);
    }

    public void DeleteThing(ItemReference item, bool hideGameObject = false)
    {
        list.Remove(item);
        if (hideGameObject)
        {
            item.gameObject.SetActive(false);
            //if(item != null)
            //    Destroy(item.gameObject);
        }
    }
}
