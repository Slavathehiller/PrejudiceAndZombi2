using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Entity
{
    public class FreshZombi : AIEntity
    {
          private float PunchCost = 3;
  //      private float BiteCost = 5;

        public override string Description
        {
            get
            {
                return "Свежеподнятый зомби. Туп. Злобен. Голоден.";
            }
        }

        protected override bool ActPrimary()
        {
            var baseResult = base.ActPrimary();
            var humanNextToMe = lcontroller.GetNeighbourObjectOfType(this, EntityType.Human);
            if (humanNextToMe != null)
            {
                if (currentActionPoint >= PunchCost) 
                { 
                    
                    StartCoroutine(Punch(humanNextToMe));
                }               
                else
                {
                    wantToMove = false;
                    return false;
                }

                return true;
            }
            return baseResult;
        }

        protected override bool ActSecondary()
        {
            var baseResult = base.ActSecondary();
            if (MaxDistance() >= 2f && wantToMove)
            {
                var nearestHuman = lcontroller.GetNearestObjectOfType(this.gameObject.transform.position, EntityType.Human);
                if (nearestHuman is null)
                    return baseResult;
                var targetPoint = NearestFreeAmongPoints(nearestHuman.SurroundPoints);
                if(Vector3.Distance(targetPoint, this.gameObject.transform.position) < 1)
                {
                    wantToMove = false;
                    return true;
                }
                StartCoroutine(Move(targetPoint));
                return true;
            }
            return baseResult;
        }

        protected override bool ActOther()
        {
            var baseResult = base.ActOther();
            if (MaxDistance() >= 1.5f && wantToMove)
            {
                StartCoroutine(RandomMove());
                return true;
            }
            return baseResult;
        }

        public IEnumerator Punch(BaseEntity entity)
        {
            isActing = true;
            currentActionPoint -= PunchCost;
            gameObject.transform.LookAt(entity.transform.position);
            econtroller.animator.SetTrigger("Punch");
            yield return new WaitForSeconds(1f);
            ProceedMeleeAttack(entity);
            yield return new WaitForSeconds(2f);
            isActing = false;
        }

        public override void TakeAttack(MeleeAttackResult attackResult)
        {
            base.TakeAttack(attackResult);
            if (CurrentHealth <= 0)
                return;
            gameObject.transform.LookAt(attackResult.AttackPoint);
            if (attackResult.Success)
            {
                econtroller.animator.SetTrigger("Hit");
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            States = new EntityStates()
            {
                inStrength = 4,
                inDexterity = 4,
                inAgility = 4,
                inConstitution = 5,
                inIntellect = 3,
                inConcentration = 7,
                inPerception = 4
            };
            base.Start();
            Name = "Свежий зомби";
            Type = EntityType.Zombie;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        public override InteractableType getType()
        {
            return InteractableType.Enemy;
        }

        public override InteractableCommand[] getCommands()
        {
            return new InteractableCommand[3] { InteractableCommand.Punch, InteractableCommand.Kick, InteractableCommand.Stab };
        }
    }
}
