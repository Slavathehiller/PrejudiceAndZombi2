using Assets.Scripts.Entity;
using UnityEngine;

public class TacticalItem : MonoBehaviour
{

    public ThingReference thingRef;

    public Sprite image;

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

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var nearObjects = other.gameObject.GetComponent<Character>().pcontroller.nearObjects;
            nearObjects.DeleteThing(thingRef);
        }
    }

}