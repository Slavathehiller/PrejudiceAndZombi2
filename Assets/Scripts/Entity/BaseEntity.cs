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
        public EntityStats Stats;

        public RightHandCell rightHandCell
        {
            get
            {
                if (_rightHandCell is null)
                    _rightHandCell = GameObject.FindGameObjectWithTag("RightHandCell").GetComponent<RightHandCell>();
                return _rightHandCell;
            }
        }

        public RightHandCell _rightHandCell;

        [NonSerialized]
        public CapsuleCollider Collider;

        public bool isActive = true;
        public bool isActing = false;
        public bool isMyTurn = false;
        public float currentActionPoint;
        public string Name = "";
        public Sprite portrait;
        [HideInInspector]
        public EntityType Type;
        public int Side;
        public LifeController lcontroller;
        public Vector3[] _SurroundPoints;
        public Inventory inventory;
        public GameObject centerOfMass;
        public AudioSource audioSource;
        public AudioClip painScream;
        public AudioClip punchSound;
        public AudioClip missedPunchSound;


        public abstract EntityController econtroller { get; }

        public virtual string Description
        {
            get
            {
                return "Нет описания.";
            }
        }

        public float MaxHealth
        {
            get
            {
                return Stats.MaxHealth;
            }
        }

        public float CurrentHealth
        {
            get { return Stats.CurrentHealth; }
            set { Stats.CurrentHealth = value; }
        }

        public float MeleeAbility
        {
            get
            {
                var result = Stats.Dexterity / 2 + Stats.Agility / 2;

                return result;
            }
        }

        public float MeleeCritChance
        {
            get
            {
                var result = Stats.Dexterity + Stats.Intellect / 2;

                return result;
            }
        }

        public float RangedAbility
        {
            get
            {
                var result = Stats.Perceprion * 2 + Stats.Dexterity + Stats.Concentration + 40;

                return result;
            }
        }

        public float RangedCritChance
        {
            get
            {
                var result = Stats.Perceprion + Stats.Intellect / 2 + Stats.Concentration / 2;

                return result;
            }
        }

        public float PureMeleeDamage
        {
            get
            {
                double result = Stats.Strength / 2;
                result = Math.Round(result * Random.Range(0.75f, 1.26f) * 100) / 100;
                return (float)result;
            }
        }


        public float Initiative
        {
            get
            {
                var result = Stats.Agility;

                return result;
            }
        }

        public float MaxActionPoint
        {
            get
            {
                var result = Stats.Concentration + Stats.Agility / 2;

                return result;
            }
        }

        public float IncomeActionPoint
        {
            get
            {
                var result = Stats.Agility / 3 + Stats.Dexterity / 3;

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
                inventory.TryRemoveItem(RightHandItem);
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
            rightHandCell.itemIn = null;
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
                audioSource.PlayOneShot(punchSound);
            }
            else
            {
                Debug.Log(Name + " промахивается.");
                audioSource.PlayOneShot(missedPunchSound);
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
            weaponModifier.CritModifier += RangedCritChance;
            econtroller.ShootAtPoint(((RangedWeapon)RightHandItem).bulletSpawner.transform, targetPoint, weaponModifier, ammo, enemy);
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
            if (CurrentHealth <= 0)
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
            CurrentHealth -= damageAmount;
            audioSource.PlayOneShot(painScream);
            if (CurrentHealth <= 0)
            {
                isActive = false;
            }
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            currentActionPoint = MaxActionPoint;
            ResetState();
            CurrentHealth = MaxHealth;
            Collider = GetComponent<CapsuleCollider>();
            audioSource = GetComponent<AudioSource>();
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
            if (CurrentHealth <= 0)
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
