using Assets.Scripts.Interchange;
using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public class CharacterT : BaseEntity, ICharacter
    {
        [SerializeField] private PlayerController _pcontroller;
        int PunchCost = 3;
        int KickCost = 5;

        public PlayerController Pcontroller => _pcontroller;
        public override EntityController econtroller
        {
            get { return _pcontroller; }
        }

        Inventory ICharacter.inventory
        {
            get
            {
                return inventory;
            }
        }

        public PrefabsController prefabsController 
        {
            get
            {
                return _pcontroller.prefabsController;
            }
        }

        public float armor
        {
            get
            {
                return inventory.Armor;
            }
        }

        EntityStats ICharacter.Stats { get => Stats; set => Stats = value; }

        public CharacterTransferData TransferData
        {
            get
            {
                return new CharacterTransferData
                {
                    Stats = this.Stats,
                    Inventory = this.inventory.TransferData,
                    RightHand = this.RightHandItem is null ? null : this.RightHandItem.TransferData,
                    Sack = Global.character.Sack is null ? new List<ItemTransferData>() : Global.character.Sack
                };
            }
        }

        public override void StartTurn()
        {
            base.StartTurn();
            _pcontroller.Ancoring(false);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            Stats = new EntityStats() { 
                inStrength = 8, 
                inDexterity = 8, 
                inAgility = 8, 
                inConstitution = 5, 
                inIntellect = 8, 
                inConcentration = 3, 
                inPerception = 6 };
            base.Start();
            _pcontroller = GetComponent<PlayerController>();

            if (Global.lastStateOnLoad == StateOnLoad.LoadFromStrategy)
            {
                Global.ReloadCharacter(this);                
            }
        }

        private void Awake()
        {
            Name = "Выживший";
            Type = EntityType.Human;
            Side = 0;
        }

        public override void TakeAttack(MeleeAttackResult attackResult)
        {
            base.TakeAttack(attackResult);
            if (CurrentHealth <= 0)
                return;
            gameObject.transform.LookAt(attackResult.AttackPoint);
            if (attackResult.Success)
            {
                _pcontroller.animator.SetTrigger("MeleeHit");
            }
            else
            {
                _pcontroller.animator.SetTrigger("MeleeDodge");
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
            if (_pcontroller.MoveIfPossible(targetPoint))
            {
                var distance = Vector3.Distance(gameObject.transform.position, targetPoint);
                if (distance >= 1)
                {
                    currentActionPoint -= distance * MovePerAP();
                    while (_pcontroller.agent.hasPath)
                    {
                        yield return null;
                    }
                }
            }
            isActing = false;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (!isActing)
                return;
        }

        public void SkipTurn()
        {
            if (!isActing && isMyTurn)
            {
                isActing = false;
                isActive = false;
                isMyTurn = false;
                _pcontroller.Ancoring(true);
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
            gameObject.transform.LookAt(_pcontroller.selectedObject.GetPosition());
            _pcontroller.animator.SetTrigger("Punch");
            yield return new WaitForSeconds(0.9f);            
            ProceedMeleeAttack(_pcontroller.SelectedEnemy);
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
            gameObject.transform.LookAt(_pcontroller.selectedObject.GetPosition());
            _pcontroller.animator.SetTrigger("Stab");
            yield return new WaitForSeconds(0.2f);
            if (RightHandItem is BaseWeapon)
                ProceedMeleeAttack(_pcontroller.SelectedEnemy, ((BaseWeapon)RightHandItem).MeleeAttackModifier);
            else
                ProceedMeleeAttack(_pcontroller.SelectedEnemy);
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
            gameObject.transform.LookAt(_pcontroller.selectedObject.GetPosition());
            _pcontroller.animator.SetTrigger("Kick");
            yield return new WaitForSeconds(0.3f);
            var modifier = new MeleeAttackModifier();
            modifier.damage = PureMeleeDamage / 2;
            ProceedMeleeAttack(_pcontroller.SelectedEnemy, modifier);
            yield return new WaitForSeconds(1.183f);
            isActing = false;
        }

        public void Shoot()
        {
            if (isActing)
                return;
            if (currentActionPoint < ((RangedWeapon)RightHandItem).ShootCost)
            {
                Debug.Log("Не хватает очков декйствия");
                return;
            }
            StartCoroutine(shoot());
        }

        private IEnumerator shoot()
        {
            isActing = true;
            var weapon = (RightHandItem as RangedWeapon);
            if (weapon.CanFire())
            {
                currentActionPoint -= weapon.ShootCost;
                gameObject.transform.LookAt(_pcontroller.selectedObject.GetPosition());
                _pcontroller.animator.SetTrigger("Shot");
                yield return new WaitForSeconds(0.05f);
                ProceedRangedAttack(_pcontroller.SelectedEnemy, weapon.rangedAttackModifier, weapon.magazine.CurrentAmmoData);
                yield return new WaitForSeconds(0.28f);
            }
            else
            {
                Debug.Log("Нет патронов");
            }
            isActing = false;
        }

        public override InteractableCommand[] getCommands()
        {
            return new InteractableCommand[0];
        }

        public void RemoveFromNearObjects(ItemReference item, bool hideGameObject = false)
        {
            GetComponent<NearObjects>().DeleteThing(item, hideGameObject);
        }

        public void PickUpItem(ItemReference item)
        {
            _pcontroller.PickUpItem(item);
        }

        public void DropItem()
        {
            DropRightItem();
        }
    }
}
