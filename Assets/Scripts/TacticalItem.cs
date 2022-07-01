using Assets.Scripts.Entity;
using UnityEngine;



public abstract class TacticalItem : Item
{   
    public CellSize size;
    public SpecType spec = SpecType.Universal;

    public Vector2 sizeInHand;

    private Light _light;

    public Rigidbody rb;
    public BoxCollider boxCollider;

    protected override void Awake()
    {
        base.Awake();
        _light = GetComponent<Light>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Drop()
    {
        gameObject.SetActive(true);
        transform.SetParent(null);
        rb.isKinematic = false;
        boxCollider.enabled = true;

        if (itemRef != null)
        {
            itemRef.character.inventory.TryRemoveItem(itemRef.thing.GetComponent<TacticalItem>());
            itemRef.character.RemoveFromNearObjects(itemRef, true);
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
