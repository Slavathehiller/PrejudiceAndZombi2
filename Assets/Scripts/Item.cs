using System.Collections;
using System.Collections.Generic;
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
    private PrefabsController prefabsController;

    static Item()
    {
        defaultSize = new Vector2(55, 40);
    }

    protected virtual void Awake()
    {
        prefabsController = GameObject.Find("PrefabsController").GetComponent<PrefabsController>();
        var pref = Instantiate(prefabsController.thingRef, null);
        var refItem = pref.GetComponent<ItemReference>();
        refItem.image.sprite = image;
        refItem.thing = gameObject;
        itemRef = refItem;
        itemRef.canvasGroup.blocksRaycasts = true;
        itemRef.gameObject.SetActive(false);
    }

    void Start()
    {

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

    public int Count
    {
        get
        {
            return _count;
        }
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
        if (_count < 1)
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
