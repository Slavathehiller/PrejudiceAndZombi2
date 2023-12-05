using Assets.Scripts.Entity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class LifeController : MonoBehaviour
    {
        public List<BaseEntity> Entities;
        private BaseEntity currentObject;

        public void RemoveEntity(BaseEntity entity)
        {
            Entities.Remove(entity);            
        }

        public bool AllEnemiesAreDead()
        {
            return Entities.Where(x => x.Side < 0).Count() == 0; ;
        }

        private BaseEntity GetFastestEntity()
        {
            //Debug.Log("GetFastestEntity------------------");
            BaseEntity result = null;
            foreach (var obj in Entities)
            {
                if (obj.isActive && obj.Initiative > (result?.Initiative ?? 0))
                    result = obj;
            }

            return result;
        }

        private void ResetTurn()
        {
            foreach (var obj in Entities)
            {
                obj.ResetState();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

            if (!(currentObject is null) && currentObject.isActive)
                return;
            currentObject = GetFastestEntity();
           if (!(currentObject is null))
            {
                if (currentObject is CharacterT)
                {

                }
               currentObject.StartTurn();
            }
            else
            {
                ResetTurn();
            }
        }
        public BaseEntity GetNearestObjectOfType(Vector3 startPoint, EntityType entityType)
        {
            float dist = float.PositiveInfinity;
            BaseEntity result = null;
            foreach(BaseEntity entity in Entities)
            {
                if(entity.Type == entityType)
                {
                    var currentDist = Vector3.Distance(entity.gameObject.transform.position, startPoint);
                    if(currentDist < dist)
                    {
                        currentDist = dist;
                        result = entity;
                    }
                } 
            }

            return result;
        }

        public BaseEntity GetNeighbourObjectOfType(BaseEntity self, EntityType entityType)
        {
            var surrPoints = self.SurroundPoints;           
            foreach (BaseEntity entity in Entities)
            {
                if (entity.Type == entityType)
                {
                    foreach(var point in surrPoints)
                    {
                        if(entity.AlignedPosition == point)
                        {
                            return entity;
                        }
                    }
                }
            }
            return null;
        }

        public bool isObjectHere(Vector3 point)
        {
            foreach (var entity in Entities)
            {
                if (entity.AlignedPosition.x == point.x && entity.AlignedPosition.z == point.z)
                    return true;
            }
            return false;
        }

    }
}
