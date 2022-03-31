using Assets.Scripts.Entity;
using UnityEngine;

public abstract class TacticalItem : MonoBehaviour
{

    public ItemReference thingRef;

    public Sprite image;

    public CellSize size;

    public Vector2 sizeInHand;
    public Vector2 sizeInInventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var character = other.gameObject.GetComponent<Character>();
            var nearObjects = character.pcontroller.nearObjects;
            nearObjects.AddThing(this, character);
            GetComponent<Light>().enabled = true;

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var nearObjects = other.gameObject.GetComponent<Character>().pcontroller.nearObjects;
            nearObjects.DeleteThing(thingRef, true);
            GetComponent<Light>().enabled = false;
        }
    }

}
