using Assets.Scripts;
using Assets.Scripts.Interchange;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public GameController gameController;
    public SectorObject sectorObject;
    [SerializeField]private PrefabsController _prefabsController;

    public GameObject selector;
    public GameObject cameraContainer;

    void Start()
    {
        _prefabsController = GameObject.Find("PrefabsController").GetComponent<PrefabsController>();
        selector = gameController.selector;
        cameraContainer = gameController.cameraContainer;
        if (sectorObject != null && sectorObject.mandatoryLoot != null && Global.lastStateOnLoad == StateOnLoad.StartGame)
            foreach (var prefab in sectorObject.mandatoryLoot)
            {
                gameController.AddItemToSector(this, prefab);
            }
    }

    public SectorTransferData TransferData
    {
        get
        {
            return new SectorTransferData()
            {
                sectorObject = sectorObject is null ? null : sectorObject.TransferData,
                position = transform.position,
                prefab = _prefabsController.sector
            };
        }
    }

    private void OnMouseDown()
    {
        if (gameController.UIInact)
            return;
        gameController.SetTimeNormal();
        BecameCurrent();
    }


    public void BecameCurrent()
    {
        selector.transform.SetParent(gameObject.transform);
        selector.transform.localPosition = Vector3.zero;
        StartCoroutine(MoveCamera(gameObject.transform.position));
        gameController.CurrentSector = this;
    }

    public void BecameCurrentInstant()
    {
        selector.transform.SetParent(gameObject.transform);
        selector.transform.localPosition = Vector3.zero;
        cameraContainer.transform.position = gameObject.transform.position;
        gameController.CurrentSector = this;
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
        gameController.RefreshSectorData();
    }

}
