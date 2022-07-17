using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public static class ItemFactory
    {
        public static GameObject CreateItem(GameObject prefab, GameObject parent = null, ICharacter character = null)
        {
            var obj = GameObject.Instantiate(prefab, null);
            var item = obj.GetComponent<Item>();
            if (item.isSMO)
                item.SetCount(Random.Range(1, item.MaxAmount));
            var itemRef = item.itemRef;
            itemRef.character = character;

            if (parent != null)
            {
                itemRef.transform.SetParent(parent.transform);
                itemRef.transform.localPosition = Vector3.zero;
                itemRef.transform.localScale = new Vector3(1, 1, 1);
                itemRef.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
            }
            itemRef.gameObject.SetActive(true);

            obj.SetActive(false);
            return obj;
        }
    }
}
