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
                _itemRef.count.enabled = this is SMO || this is RangedWeapon;
        }
    }

    public Sprite image;

    public CellSize size;

    public SpecType spec = SpecType.Universal;

    public Vector2 sizeInHand;
    public Vector2 sizeInInventory;

    private Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
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
