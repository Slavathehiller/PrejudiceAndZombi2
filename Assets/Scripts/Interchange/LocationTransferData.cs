using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class LocationTransferData
    {
        public string SceneName;
        public IEnumerable<SectorTransferData> sectors;
        public Vector3 currentSectorPosition;
    }
}
