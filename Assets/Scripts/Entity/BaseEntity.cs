using Assets.Scripts.Weapon;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Scripts.Entity
{
    public enum EntityType 
    {
        Human = 0,
        Zombie = 1,
        NecroMorph = 2
    }

    public abstract class BaseEntity : MonoBehaviour, IInteractableEntity
    {
        [SerializeField]
        private float inStrength = 0;
        [SerializeField]
        private float inDexterity = 0;
        [SerializeField]
        private float inAgility = 0;
        [SerializeField]
        private float inConstitution = 0;
        [SerializeField]
        private float inIntellect = 0;
        [SerializeField]
        private float inConcentration = 0;
        [SerializeField]
        private float inPerception = 0;

        [NonSerialized]
        public CapsuleCollider Collider;

        public bool isActive = true;
        public bool isActing = false;
        public bool isMyTurn = false;
        public float currentActionPoint;
        public float currentHitpoint;
        public string Name = "";
        public Sprite portrait;
        [HideInInspector]
        public EntityType Type;
        public int Side;
        public LifeController lcontroller;
        public Vector3[] _SurroundPoints;
        public Inventory inventory;
        public GameObject centerOfMass;

        public abstract EntityController econtroller { get; }


        public virtual string Description
        {
            get
            {
                return "Нет описания.";
            }
        }


        public float Strength
        {
            get
            {
                var result = inStrength;

                return result;
            }
        }

        public float Dexterity
        {
            get
            {
                var result = inDexterity;

                return result;
            }
        }

        public float Agility
        {
            get
            {
                var result = inAgility;

                return result;
            }
        }


        public float Constitution
        {
            get
            {
                var result = inConstitution;

                return result;
            }
        }

        public float Intellect
        {
            get
            {
                var result = inIntellect;

                return result;
            }
        }
        public float Concentration
        {
            get
            {
                var result = inConcentration;

                return result;
            }
        }

        public float Perceprion
        {
            get
            {
                var result = inPerception;

                return result;
            }
        }





        public float MaxHealth
        {
            get
            {
                var result = Constitution * 10;

                return result;
            }
        }

        public float MeleeAbility
        {
            get
            {
                var result = Dexterity / 2 + Agility / 2;

                return result;
            }
        }

        public float MeleeCritChance
        {
            get
            {
                var result = Dexterity + Intellect / 2;

                return result;
            }
        }


        public float RangedAbility
        {
            get
            {
                var result = Perceprion * 2 + Dexterity + Concentration + 40;

                return result;
            }
        }

        public float RangedCritChance
        {
            get
            {
                var result = Perceprion + Intellect / 2 + Concentration / 2;

                return result;
            }
        }

        public float PureMeleeDamage
        {
            get
            {
                double result = Strength / 2;
                result = Math.Round(result * Random.Range(0.75f, 1.26f) * 100) / 100;
                return (float)result;
            }
        }


        public float Initiative
        {
            get
            {
                var result = Agility;

                return result;
            }
        }

        public float MaxActionPoint
        {
            get
            {
                var result = Concentration + Agility / 2;

                return result;
            }
        }

        public float IncomeActionPoint
        {
            get
            {
                var result = Agility / 3 + Dexterity / 3;

                return result;
            }
        }

        [SerializeField]
        private TacticalItem rightHandItem;
        public TacticalItem RightHandItem
        {
            get { return rightHandItem; }
            set
            {
                rightHandItem = value;
            }
        }
        public void DropRightItem()
        {
            if (!isActing)
            {
                inventory.RemoveItem(RightHandItem);
                RemoveFromRightHand(true);
            }
        }

        public void TakeToRightHand(GameObject item)
        {
            RightHandItem = item.GetComponent<TacticalItem>();
            econtroller.PlaceToRightHand(item);
        }

        public void RemoveFromRightHand(bool drop)
        {
            econtroller.RemoveFromRightHand(drop);
            RightHandItem = null;
        }

        public void TakeToRightHandNew(GameObject weapon)
        {
            var w = Instantiate(weapon, econtroller.rightHandHandler.transform);
            RightHandItem = w.GetComponent<TacticalItem>();
        }

        public void ProceedMeleeAttack(BaseEntity enemy, MeleeAttackModifier modifier)
        {
            Debug.Log(Name + " бъет " + enemy.Name);
            var attackResult = new MeleeAttackResult(gameObject.transform.position);
            float hitChance = 50 + MeleeAbility * 5 - enemy.MeleeAbility * 5;
            bool isHit = Random.Range(1, 101) <= hitChance;
            attackResult.Success = isHit;
            if (isHit)
            {
                float damageAmount = PureMeleeDamage + modifier.damage;
                bool crit = Random.Range(1, 101) <= MeleeCritChance;
                if (crit)
                {
                    damageAmount = damageAmount * 2;
                    Debug.Log(Name + " наносит критический удар!");
                }
                attackResult.DamageAmount = damageAmount;
                //enemy.TakeDamage(damageAmount);
                Debug.Log(enemy.Name + " получает " + damageAmount + " урона.");
            }
            else
            {
                Debug.Log(Name + " промахивается.");
            }
            enemy.TakeAttack(attackResult);
        }

        public void ProceedRangedAttack(BaseEntity enemy, RangedAttackModifier weaponModifier, AmmoData ammo)
        {
            (RightHandItem as RangedWeapon).ConsumeAmmo();
            Debug.Log(Name + " стреляет в " + enemy.Name);
            var targetPoint = enemy.centerOfMass.transform.position;
            //Debug.Log(targetPoint + "Before Modified");
            var deviation_x = Random.Range(0, 100) - (RangedAbility + weaponModifier.ToHitModifier + ammo.attackModifier.ToHitModifier);
            var deviation_y = Random.Range(0, 100) - (RangedAbility + weaponModifier.ToHitModifier + ammo.attackModifier.ToHitModifier);
            if (deviation_x > 0)
            {
                var direction = Random.Range(0, 2);
                if(direction > 0)
                {
                    targetPoint.x += deviation_x / 100;
                }
                else
                {
                    targetPoint.x -= deviation_x / 100;
                }
            }
            if (deviation_y > 0)
            {
                var direction = Random.Range(0, 2);
                if (direction > 0)
                {
                    targetPoint.y += deviation_y / 100;
                }
                else
                {
                    targetPoint.y -= deviation_y / 100;
                }
            }
            //Debug.Log(targetPoint + "Modified");
            econtroller.ShootAtPoint(((RangedWeapon)RightHandItem).bulletSpawner.transform, targetPoint,weaponModifier, ammo, enemy);
        }


            public void ProceedMeleeAttack(BaseEntity enemy)
        {
            ProceedMeleeAttack(enemy, new MeleeAttackModifier());
        }

        public Vector3 AlignedPosition
        {
            get
            {
                var result = transform.position;
                result.x = Mathf.RoundToInt(result.x);
                result.z = Mathf.RoundToInt(result.z);
                result.y = 0;
                return result;
            }
        }

        public Vector3[] SurroundPoints
        {
            get
            {
                return new Vector3[8] {new Vector3(AlignedPosition.x + 1, AlignedPosition.y, AlignedPosition.z),
                                        new Vector3(AlignedPosition.x + 1, AlignedPosition.y, AlignedPosition.z - 1),
                                            new Vector3(AlignedPosition.x, AlignedPosition.y, AlignedPosition.z - 1),
                                                new Vector3(AlignedPosition.x - 1, AlignedPosition.y, AlignedPosition.z - 1),
                                                    new Vector3(AlignedPosition.x - 1, AlignedPosition.y, AlignedPosition.z),
                                                        new Vector3(AlignedPosition.x - 1, AlignedPosition.y, AlignedPosition.z + 1),
                                                            new Vector3(AlignedPosition.x, AlignedPosition.y, AlignedPosition.z + 1),
                                                                new Vector3(AlignedPosition.x + 1, AlignedPosition.y, AlignedPosition.z + 1)};
            }
        }

        public virtual float MovePerAP()
        {
            return 1;
        }

        public float MaxDistance()
        {
            return currentActionPoint / MovePerAP();
        }

        public void ResetState()
        {
            if (currentHitpoint <= 0)
                return;
            if (currentActionPoint < MaxActionPoint)
                currentActionPoint = Math.Min(currentActionPoint + IncomeActionPoint, MaxActionPoint);
            isActive = true;
        }


        public virtual void TakeAttack(MeleeAttackResult attackResult)
        {
            if (attackResult.Success)
            {
                TakeDamage(attackResult.DamageAmount);
            }
        }

        public void TakeDamage(float damageAmount)
        {
            currentHitpoint -= damageAmount;
            if (currentHitpoint <= 0)
            {
                isActive = false;
            }
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            currentActionPoint = MaxActionPoint;
            ResetState();
            currentHitpoint = MaxHealth;
            Collider = GetComponent<CapsuleCollider>();
        }
        private void LateUpdate()
        {
            _SurroundPoints = SurroundPoints;
        }

        public virtual void StartTurn()
        {
            isMyTurn = true;
            isActing = false;            
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (currentHitpoint <= 0)
            {
                isActive = false;
            }
        }

        public virtual InteractableType getType()
        {
            return InteractableType.Undefined;
        }
        public abstract InteractableCommand[] getCommands();
        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }

        public BaseEntity GetEntity()
        {
            return this;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
