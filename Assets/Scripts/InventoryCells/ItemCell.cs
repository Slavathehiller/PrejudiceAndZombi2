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
            itemIn = thing;
            item.character.RemoveFromNearObjects(item, false);

            PlaceItemToCell(item);
            item.thing.GetComponent<Light>().enabled = false;
            var oldParentCell = item.oldParent.GetComponent<ItemCell>();
            if (oldParentCell is RightHandCell)
            {
                item.RemoveFromRightHand();
            }
            if (this is RightHandCell)
            {
                item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInHand;
            }
            else
            {
                item.thing.SetActive(false);
                item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;
            }

            if (oldParentCell != null)
            {
                oldParentCell.itemIn = null;
                oldParentCell.ShowBackground(true);
            }
            ShowBackground(false);
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

    public virtual void PlaceItemToCell(ItemReference item)
    {
        item.transform.SetParent(gameObject.transform);        
        item.image.GetComponent<RectTransform>().sizeDelta = item.thing.GetComponent<Item>().sizeInInventory;
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = new Vector3(1, 1, 1);
        item.background.enabled = false;
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
