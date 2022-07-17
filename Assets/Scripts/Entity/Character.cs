using Assets.Scripts.Interchange;
using Assets.Scripts.Weapon;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public class Character : BaseEntity, ICharacter
    {
        public PlayerController pcontroller;
      //  public PrefabsController pfcontroller;
        int PunchCost = 3;
        int KickCost = 5;        

        public override EntityController econtroller
        {
            get { return pcontroller; }
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
                return pcontroller.prefabsController;
            }
        }

        public float armor
        {
            get
            {
                return inventory.Armor;
            }
        }

        public override void StartTurn()
        {
            base.StartTurn();
            pcontroller.Ancoring(false);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            Stats = new EntityStats() { 
                inStrength = 4, 
                inDexterity = 6, 
                inAgility = 8, 
                inConstitution = 5, 
                inIntellect = 8, 
                inConcentration = 3, 
                inPerception = 6 };
            base.Start();
            pcontroller = GetComponent<PlayerController>();
            Name = "Выживший";
            Type = EntityType.Human;
            Side = 0;

            if (Global.needToLoad)
            {
                LoadFromGlobal();
                Global.needToLoad = true;
            }
        }

        private void LoadFromGlobal()
        {
            EquipmentItem CheckAndInstEq(EquipmentItemTransferData data)
            {
                if (data is null)
                    return null;
                else
                {
                    var item = Instantiate(data.Prefab).GetComponent<EquipmentItem>();
                    item.itemRef.character = this;
                    var invItem = item as InventoryEquipmentItem;
                    if (invItem != null)
                    {
                        var i = 0;
                        foreach (var itemdata in data.ItemList)
                        {
                            if (itemdata != null)
                            {
                                invItem.cellList[i].itemIn = Instantiate(itemdata.Prefab).GetComponent<TacticalItem>();
                                invItem.cellList[i].itemIn.SetCount(itemdata.Count);

                                if (itemdata is RangedWeaponTransferData)
                                {
                                    var magazine = Instantiate((itemdata as RangedWeaponTransferData).Magazine.Prefab).GetComponent<WeaponMagazine>();
                                    magazine.CurrentAmmoCount = (itemdata as RangedWeaponTransferData).Magazine.CurrentAmmoCount;
                                    magazine.CurrentAmmoData = (itemdata as RangedWeaponTransferData).Magazine.CurrentAmmoData;
                                    (invItem.cellList[i].itemIn as RangedWeapon).magazine = magazine;
                                    magazine.gameObject.SetActive(false);
                                }

                                invItem.cellList[i].PlaceItemToCell(invItem.cellList[i].itemIn.itemRef);
                                invItem.cellList[i].itemIn.itemRef.character = this;
                                invItem.cellList[i].itemIn.itemRef.gameObject.SetActive(true);
                                invItem.cellList[i].itemIn.gameObject.SetActive(false);
                            }
                            i++;
                        }
                        return invItem;
                    }
                    

                    return item;
                }                    
            }

            EquipmentItem CheckAndInstAr(ItemTransferData data)
            {
                if (data is null)
                    return null;
                else
                {
                    var item = Instantiate(data.Prefab).GetComponent<EquipmentItem>();
                    item.itemRef.character = this;
                    return item;
                }
            }

            Stats = Global.character.Stats;
            inventory.EquipItem(CheckAndInstEq(Global.character.Inventory.shirt), SpecType.EqShirt);
            inventory.EquipItem(CheckAndInstEq(Global.character.Inventory.belt), SpecType.EqBelt);
            inventory.EquipItem(CheckAndInstEq(Global.character.Inventory.pants), SpecType.EqPants);
            inventory.EquipItem(CheckAndInstAr(Global.character.Inventory.helmet), SpecType.Helmet);
            inventory.EquipItem(CheckAndInstAr(Global.character.Inventory.chestArmor), SpecType.ChestArmor);
            inventory.EquipItem(CheckAndInstAr(Global.character.Inventory.gloves), SpecType.Gloves);
            inventory.EquipItem(CheckAndInstAr(Global.character.Inventory.boots), SpecType.Boots);
        }

        public override void TakeAttack(MeleeAttackResult attackResult)
        {
            base.TakeAttack(attackResult);
            if (CurrentHealth <= 0)
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
            ProceedMeleeAttack(pcontroller.SelectedEnemy);
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
            if (RightHandItem is BaseWeapon)
                ProceedMeleeAttack(pcontroller.SelectedEnemy, ((BaseWeapon)RightHandItem).MeleeAttackModifier);
            else
                ProceedMeleeAttack(pcontroller.SelectedEnemy);
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
            ProceedMeleeAttack(pcontroller.SelectedEnemy, modifier);
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
                gameObject.transform.LookAt(pcontroller.selectedObject.GetPosition());
                pcontroller.animator.SetTrigger("Shot");
                yield return new WaitForSeconds(0.05f);
                ProceedRangedAttack(pcontroller.SelectedEnemy, weapon.rangedAttackModifier, weapon.magazine.CurrentAmmoData);
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
            pcontroller.PickUpItem(item);
        }
    }
}
