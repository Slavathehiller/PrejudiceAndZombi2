using Assets.Scripts;
using Assets.Scripts.Entity;
using Assets.Scripts.Weapon;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entity
{

    public class Character : BaseEntity
    {
        public PlayerController pcontroller;
        public PrefabsController pfcontroller;
        int PunchCost = 3;
        int KickCost = 5;

        public override EntityController econtroller
        {
            get { return pcontroller; }
        }

        public override void StartTurn()
        {
            base.StartTurn();
            pcontroller.Ancoring(false);
        }
        private void RefreshStates()
        {
          //  InterfaceController.SetStats(this);
          //  InterfaceController.SetSkills(this);
        }

        private void RefreshAP()
        {
          //  InterfaceController.SetAP(this);
        }

        private void RefreshSkills()
        {
           // InterfaceController.SetSkills(this);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            pcontroller = GetComponent<PlayerController>();
            Name = "Выживший";
            Type = EntityType.Human;
            Side = 0;
           // TakeToRightHandNew(pfcontroller.kitchenKnife);
        }

        public override void TakeAttack(AttackResult attackResult)
        {
            base.TakeAttack(attackResult);
            if (currentHitpoint <= 0)
                return;
            gameObject.transform.LookAt(attackResult.AttackPoint);
            if (attackResult.Success)
            {
                pcontroller.animator.SetTrigger("MeleeHit");
            }
            else
            {
                pcontroller.animator.SetTrigger("MeleeDodge");
            }

        }

        public void Move(Vector3 targetPoint)
        {
            if (!isActing && isMyTurn)
                StartCoroutine(Moving(targetPoint));
        }

        IEnumerator Moving(Vector3 targetPoint)
        {
            isActing = true;
            if (pcontroller.MoveIfPossible(targetPoint))
            {
                currentActionPoint -= Vector3.Distance(gameObject.transform.position, targetPoint) * MovePerAP();
                while (pcontroller.agent.hasPath)
                {
                    yield return null;
                }
            }
            isActing = false;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (!isActing)
                return;

            RefreshStates();
            RefreshAP();

        }

        public void SkipTurn()
        {
            if (!isActing && isMyTurn)
            {
                isActing = false;
                isActive = false;
                isMyTurn = false;
                pcontroller.Ancoring(true);
            }
        }        

        public void Punch()
        {
            if (isActing || currentActionPoint < PunchCost)
                return;
            StartCoroutine(punch());  
        }

        private IEnumerator punch()
        {
            isActing = true;
            currentActionPoint -= PunchCost;
            gameObject.transform.LookAt(pcontroller.selectedObject.GetPosition());
            pcontroller.animator.SetTrigger("Punch");
            yield return new WaitForSeconds(0.9f);
            ProceedMeleeAttack(pcontroller.selectedObject.GetEntity());
            yield return new WaitForSeconds(1.1f);
            isActing = false;

        }


        public void Stab()
        {
            if (isActing || currentActionPoint < PunchCost)
                return;
            StartCoroutine(stab());
        }

        private IEnumerator stab()
        {
            isActing = true;
            currentActionPoint -= PunchCost;
            gameObject.transform.LookAt(pcontroller.selectedObject.GetPosition());
            pcontroller.animator.SetTrigger("Stab");
            yield return new WaitForSeconds(0.2f);
            var modifier = new MeleeAttackModifier();
            if (RightHandWeapon is BaseWeapon)
                modifier = ((BaseWeapon)RightHandWeapon).MeleeAttackModifier;
            ProceedMeleeAttack(pcontroller.selectedObject.GetEntity(), modifier);           
            yield return new WaitForSeconds(1.84f);
            isActing = false;
        }


        public void Kick()
        {
            if (isActing || currentActionPoint < KickCost)
                return;
            StartCoroutine(kick());
        }

        private IEnumerator kick()
        {
            isActing = true;
            currentActionPoint -= KickCost;
            gameObject.transform.LookAt(pcontroller.selectedObject.GetPosition());
            pcontroller.animator.SetTrigger("Kick");
            yield return new WaitForSeconds(0.3f);
            var modifier = new MeleeAttackModifier();
            modifier.damage = PureMeleeDamage / 2;
            ProceedMeleeAttack(pcontroller.selectedObject.GetEntity(), modifier);
            yield return new WaitForSeconds(1.183f);
            isActing = false;
        }


        public override InteractableCommand[] getCommands()
        {
            return new InteractableCommand[0];
        }
    }
}
