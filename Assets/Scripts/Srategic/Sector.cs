using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public GameController gameController;
    public SectorObject sectorObject;

    private GameObject selector;
    private GameObject cameraContainer;

    // Start is called before the first frame update
    void Start()
    {
        selector = gameController.selector;
        cameraContainer = gameController.cameraContainer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameController.isLocked())
            return;
        selector.transform.SetParent(gameObject.transform);
        selector.transform.localPosition = Vector3.zero;
        StartCoroutine(MoveCamera(gameObject.transform.position));
        gameController.currentSector = this;
    }

    IEnumerator MoveCamera(Vector3 point)
    {
        gameController.cameraMoving = true;        
        while (Vector3.Distance(cameraContainer.transform.position, point) > 0.01f)
        {
            var movePoint = Vector3.MoveTowards(cameraContainer.transform.position, point, 0.01f);
            cameraContainer.transform.position = movePoint;
            yield return new WaitForSeconds(0.01f);
        }
        gameController.cameraMoving = false;
    }

}
