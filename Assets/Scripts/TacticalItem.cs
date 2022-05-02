using Assets.Scripts.Entity;
using UnityEngine;


public enum SpecType
{
    Universal = 0,
    InBelt = 1,
    Shoulder = 2
}
public abstract class TacticalItem : MonoBehaviour
{
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
                _itemRef.count.enabled = this is SMO || this is RangedWeapon || this is WeaponMagazine;
        }
    }

    public Sprite image;
    
    public CellSize size;

    public SpecType spec = SpecType.Universal;

    public Vector2 sizeInHand;
    public Vector2 sizeInInventory;

    private Light _light;

    public Rigidbody rb;
    public BoxCollider boxCollider;

    void Start()
    {
        _light = GetComponent<Light>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Drop()
    {
        gameObject.SetActive(true);
        transform.SetParent(null);
        rb.isKinematic = false;
       // rb.useGravity = true;
        boxCollider.enabled = true;

        if (itemRef != null)
        {
            itemRef.character.inventory.RemoveItem(itemRef.thing.GetComponent<TacticalItem>());
            itemRef.character.gameObject.GetComponent<NearObjects>().DeleteThing(itemRef, true);
        }



    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var character = other.gameObject.GetComponent<Character>();
            var nearObjects = character.pcontroller.nearObjects;
            nearObjects.AddItem(this, character);
            _light.enabled = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var nearObjects = other.gameObject.GetComponent<Character>().pcontroller.nearObjects;
            nearObjects.DeleteThing(itemRef, true);
            _light.enabled = false;
        }
    }

}
