using Assets.Scripts.Interchange;
using UnityEngine;

public enum SpecType
{
    Universal = 0,
    InBelt = 1,
    Shoulder = 2,
    EqShirt = 3,
    EqBelt = 4,
    EqPants = 5,
    Helmet = 6,
    ChestArmor = 7,
    Boots = 8,
    Gloves = 9
}

public abstract class Item : MonoBehaviour
{
    public virtual string Name {get;set;}
    public string Description;
    public Sprite image;
    public Vector2 sizeInInventory;
    public readonly static Vector2 defaultSize;
    public PrefabsController prefabsController;
    public GameObject prefab;

    static Item()
    {
        defaultSize = new Vector2(30, 30);
    }

    public virtual ItemTransferData TransferData
    {
        get
        {
            return new ItemTransferData
            {
                Prefab = prefab,
                Count = _count
            };
        }
    }

    protected virtual void Awake()
    {
        prefabsController = GameObject.Find("PrefabsController").GetComponent<PrefabsController>();
        var pref = Instantiate(prefabsController.thingRef, null);
        var refItem = pref.GetComponent<ItemReference>();
        refItem.transform.localScale = new Vector3(1, 1, 1);
        refItem.image.sprite = image;
        refItem.thing = gameObject;
        itemRef = refItem;
        itemRef.canvasGroup.blocksRaycasts = true;
        itemRef.gameObject.SetActive(false);
    }

    [SerializeField]
    ItemReference _itemRef;
    public ItemReference itemRef
    {
        get
        {
            return _itemRef;
        }
        set
        {
            _itemRef = value;
            if (_itemRef != null)
                _itemRef.count.enabled = isSMO || this is RangedWeapon || this is WeaponMagazine;
        }
    }

    [SerializeField]
    protected int _count;

    public int GetCount()
    {
        return _count;
    }

    protected int _maxAmount;

    public int MaxAmount
    {
        get
        {
            return _maxAmount;
        }
    }

    public bool isSMO
    {
        get
        {
            return MaxAmount > 1;
        }
    }

    public void SetCount(int number)
    {
        _count = number;
        CheckNum();
    }
    public void Add(int number = 1)
    {
        _count += number;
        CheckNum();
    }

    private void CheckNum()
    {
        if (itemRef != null)
            itemRef.count.text = _count.ToString();
        if (_count < 1 && isSMO)
        {
            itemRef.character.inventory.TryRemoveItem(this);
            Destroy(itemRef.gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual void Update()
    {
        if(itemRef != null && isSMO)
            itemRef.count.text = _count.ToString();
    }

}
