
using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class SectorTransferData
    {
        public Vector3 position;
        public SectorObjectTransferData sectorObject;
        public GameObject prefab;

        public Sector Restore(GameController gameController)
        {
            var sector = Object.Instantiate(prefab).GetComponent<Sector>();
            sector.transform.position = position;
            sector.gameController = gameController;
            sector.selector = gameController.selector;
            sector.cameraContainer = gameController.cameraContainer;
            if (sectorObject != null)
            {
                var obj = sectorObject.Restore();
                obj.transform.SetParent(sector.transform);
                obj.transform.localPosition = Vector3.zero;
                sector.sectorObject = obj;
                sector.gameController.AddItemsToSector(sector, sectorObject.sack);
            }
            return sector;
        }
    }
}
