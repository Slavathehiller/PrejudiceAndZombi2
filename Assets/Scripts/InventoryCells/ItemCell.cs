using UnityEngine;
using UnityEngine.EventSystems;


public enum CellSize{
    Small = 1,
    Medium = 2,
    Large = 4
}
public class ItemCell : MonoBehaviour, IDropHandler
{
    public CellSize size;

    public SpecType spec = SpecType.Universal;

    public GameObject background;

    public TacticalItem itemIn;
    public virtual void OnDrop(PointerEventData eventData)
    {
        TryToDrop(eventData);
    }

    protected bool TryToDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var thing = item.thing.GetComponent<TacticalItem>();
        if (thing is null)
            return false;
        if (thing.isSMO && itemIn != null && thing.GetType() == itemIn.GetType()
            && itemIn.MaxAmount > itemIn.GetCount())
        {
            var addingCount = Mathf.Min(thing.GetCount(), itemIn.MaxAmount - itemIn.GetCount());
            itemIn.Add(addingCount);
            thing.Add(-addingCount);
            item.character.RemoveFromNearObjects(item, false);
        }

        if (item != null && thing.size <= size && (spec == SpecType.Universal || spec == thing.spec) && itemIn is null)
        {
            item.character.RemoveFromNearObjects(item, false);
            item.thing.GetComponent<Light>().enabled = false;
            var oldParentCell = item.oldParent.GetComponent<ItemCell>();
            if (oldParentCell is RightHandCell)
            {
                item.RemoveFromRightHand();
            }

            PlaceItemToCell(item);

            if (oldParentCell != null)
            {
                oldParentCell.itemIn = null;
                oldParentCell.ShowBackground(true);
            }

            var _oldParent = item.oldParent.GetComponent<SectorSackCell>();
            if (_oldParent != null)
                _oldParent.RemoveFromSector(item);

            return true;
        }
        else
        {
            return false;
        }
       
    }

    public void PlaceItemToCell(Item item)
    {
        if (item != null)
            PlaceItemToCell(item.itemRef);
    }

    public virtual void PlaceItemToCell(ItemReference item)
    {
        item.transform.SetParent(gameObject.transform); 
        var thing = item.thing.GetComponent<Item>();
        itemIn = thing as TacticalItem; 

        if (this is RightHandCell)
        {
            item.image.GetComponent<RectTransform>().sizeDelta = (thing as TacticalItem).sizeInHand;
        }
        else
        {
            item.thing.SetActive(false);
            item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;
        }

        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = new Vector3(1, 1, 1);
        item.background.enabled = false;
        item.gameObject.SetActive(true);
        ShowBackground(false);

    }

    public void ShowBackground(bool on)
    {
        if(background != null)
        {
            background.SetActive(on);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
