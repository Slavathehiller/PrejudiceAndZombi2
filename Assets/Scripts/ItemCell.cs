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
    public virtual void OnDrop(PointerEventData eventData)
    {
        TryToDrop(eventData);
    }

    protected bool TryToDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var thing = item.thing.GetComponent<TacticalItem>();

        var thingInCell = item.character.inventory.ItemInCell(this);
        if (thing.isSMO && thingInCell != null && thing.GetType() == thingInCell.GetType()
            && thingInCell.MaxAmount > thingInCell.Count)
        {
            var addingCount = Mathf.Min(thing.Count, thingInCell.MaxAmount - thingInCell.Count);
            thingInCell.Add(addingCount);
            thing.Add(-addingCount);
            item.character.RemoveFromNearObjects(item, false);
        }

        if (item != null && thing.size <= size && (spec == SpecType.Universal || spec == thing.spec) && item.character.inventory.IsCellEmpty(this))
        {
            item.character.inventory.AddItem(thing);
            item.character.RemoveFromNearObjects(item, false);
            item.transform.SetParent(gameObject.transform);
            item.transform.localPosition = Vector3.zero;
            item.thing.GetComponent<Light>().enabled = false;
            var oldParentCell = item.oldParent.GetComponent<ItemCell>();
            if (oldParentCell is RightHandCell)
            {
                item.RemoveFromRightHand();
            }
            if (!(this is RightHandCell))
            {
                item.thing.SetActive(false);
                item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;
            }
            else
                item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInHand;
            if (oldParentCell != null)
                oldParentCell.ShowBackground(true);
            ShowBackground(false);
            return true;
        }
        else
        {
            return false;
        }
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
