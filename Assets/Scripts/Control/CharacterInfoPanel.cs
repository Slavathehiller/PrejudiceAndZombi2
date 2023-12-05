using Assets.Scripts.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BaseStats
{
    Strength = 0,
    Dexterity = 1,
    Agility = 2,
    Constitution = 3,
    Intellect = 4,
    Concentration = 5,
    Perception = 6,
    MeleeAbility = 7,
    MeleeCritChance = 8,
    RangedAbility = 9,
    RangedCritChance = 10,
    PureMeleeDamage = 11,
    Initiative = 12,
    MaxActionPoint = 13,
    IncomeActionPoint = 14,
    MaxHealth = 15,
    Stealth = 16,
    Observation = 17,
    HealthRestoreRatio = 18,
    EnergyRestoreRatio = 19,
    HungerIncreaseRatio = 20,
    ThirstIncreaseRatio = 21
}

public class CharacterInfoPanel : MonoBehaviour
{

    private Dictionary<BaseStats, string> StatsDescriptions = new Dictionary<BaseStats, string>();

    [SerializeField] private Text StatsDescription;

    [SerializeField] private Image Portrait;
    [SerializeField] private Text Name;

    [SerializeField] private Text Strength;
    [SerializeField] private Text Dexterity;
    [SerializeField] private Text Agility;
    [SerializeField] private Text Constitution;
    [SerializeField] private Text Intellect;
    [SerializeField] private Text Concentration;
    [SerializeField] private Text Perception;

    [SerializeField] private Text MeleeAbility;
    [SerializeField] private Text MeleeCritChance;
    [SerializeField] private Text RangedAbility;
    [SerializeField] private Text RangedCritChance;
    [SerializeField] private Text PureMeleeDamage;
    [SerializeField] private Text Initiative;
    [SerializeField] private Text MaxActionPoint;
    [SerializeField] private Text IncomeActionPoint;
    [SerializeField] private Text MaxHealth;
    [SerializeField] private Text Stealth;
    [SerializeField] private Text Observation;
    [SerializeField] private Text HealthRestoreRatio;
    [SerializeField] private Text EnergyRestoreRatio;
    [SerializeField] private Text HungerIncreaseRatio;
    [SerializeField] private Text ThirstIncreaseRatio;

    [SerializeField] private GameController gameController;

    private ICharacter Character;


    private void Awake()
    {
        Character = gameController.character;
        StatsDescriptions.Add(BaseStats.Strength, "Физическая сила персонажа. Влияет на наносимый урон в ближнем бою и максимальный переносимый вес.");
        StatsDescriptions.Add(BaseStats.Dexterity, "Координация движения и скорость реакции персонажа. Влияет на боевые умения, скорость восстановления ОД и скрытность.");
        StatsDescriptions.Add(BaseStats.Agility, "Быстрота и проворство персонажа. Влияет на инициативу в бою и скорость восстановления ОД");
        StatsDescriptions.Add(BaseStats.Constitution, "Физическое здоровье персонажа. Влияет на максимальное количество и скорость восстановления здоровья и бодрости.");
        StatsDescriptions.Add(BaseStats.Intellect, "Умственные способности и проницательность персонажа. Влияет на умение находить сокрытое и скрываться самому, а так же на шанс нанести критический урон в бою.");
        StatsDescriptions.Add(BaseStats.Concentration, "Способность персонажа концентрироваться на задаче. Влияет на максимальное количество ОД и умения дистанционного боя.");
        StatsDescriptions.Add(BaseStats.Perception, "Острота зрения и наблюдательность персонажа. Влияет на умение находить сокрытое и дистанционный бой.");
        StatsDescriptions.Add(BaseStats.MeleeAbility, "Способность драться в ближнем бою. Зависит от ловкости и проворности.");
        StatsDescriptions.Add(BaseStats.MeleeCritChance, "Шанс нанести критический урон в ближнем бою. Зависит от ловкости и интеллекта.");
        StatsDescriptions.Add(BaseStats.RangedAbility, "Умение пользоваться оружием дальнего боя. Зависит от восприятия, ловкости и концентрации.");
        StatsDescriptions.Add(BaseStats.RangedCritChance, "Шанс нанести критический урон в дальнем бою. Зависит от восприятия, интеллекта и концентрации.");
        StatsDescriptions.Add(BaseStats.PureMeleeDamage, "Урон наносимый голыми руками. Зависит от силы.");
        StatsDescriptions.Add(BaseStats.MaxActionPoint, "Максимальное количество ОД. Зависит от концентрации и проворности.");
        StatsDescriptions.Add(BaseStats.IncomeActionPoint, "Количество ОД, которое восстанавливается за ход. Зависит от проворности и ловкости.");
        StatsDescriptions.Add(BaseStats.MaxHealth, "Максимальное количество здоровья. Зависит от выносливости.");
        StatsDescriptions.Add(BaseStats.Initiative, "Влияет на очерёдность хода. Зависит от проворности.");
        StatsDescriptions.Add(BaseStats.Stealth, "Способность оставаться не замеченым. Зависит от ловкости, восприятия и интеллекта ");
        StatsDescriptions.Add(BaseStats.Observation, "Способность находить сокрытое. Зависит от восприятия и интеллекта ");
        StatsDescriptions.Add(BaseStats.HealthRestoreRatio, "Скорость восстановления здоровья. Зависит от выносливости");
        StatsDescriptions.Add(BaseStats.EnergyRestoreRatio, "Скорость восстановления бодрости. Зависит от выносливости");
        StatsDescriptions.Add(BaseStats.HungerIncreaseRatio, "Скорость накопления голода. Зависит от выносливости");
        StatsDescriptions.Add(BaseStats.ThirstIncreaseRatio, "Скорость накопления жажды. Зависит от выносливости");


    }

    public void RefreshStats()
    {
        Name.text = Character.Name;
        Portrait.sprite = Character.Portrait;
        CharacterS CharacterS = Character as CharacterS;

        Strength.text = Character.Stats.inStrength.ToString();
        Dexterity.text = Character.Stats.inDexterity.ToString();
        Agility.text = Character.Stats.inAgility.ToString();
        Constitution.text = Character.Stats.inConstitution.ToString();
        Intellect.text = Character.Stats.inIntellect.ToString();
        Concentration.text = Character.Stats.inConcentration.ToString();
        Perception.text = Character.Stats.inPerception.ToString();
        //MeleeAbility.text = BaseEntity.MeleeAbility.ToString();
       // MeleeCritChance.text = BaseEntity.MeleeCritChance.ToString();
       // RangedAbility.text = BaseEntity.RangedAbility.ToString();
      //  RangedCritChance.text = BaseEntity.RangedCritChance.ToString();
      //  PureMeleeDamage.text = BaseEntity.PureMeleeDamage.ToString();
      //  Initiative.text = BaseEntity.Initiative.ToString();
      //  MaxActionPoint.text = BaseEntity.MaxActionPoint.ToString();
      //  IncomeActionPoint.text = BaseEntity.IncomeActionPoint.ToString();
       // MaxHealth.text = BaseEntity.MaxHealth.ToString();
        Stealth.text = CharacterS.Stealth.ToString();
        Observation.text = Math.Round(CharacterS.Observation, 2).ToString();
        HealthRestoreRatio.text = Math.Round(CharacterS.HealthRestoreRatio, 2).ToString();
        EnergyRestoreRatio.text = Math.Round(CharacterS.EnergyRestoreRatio, 2).ToString();
        HungerIncreaseRatio.text = Math.Round(CharacterS.HungerIncreaseRatio, 2).ToString();
        ThirstIncreaseRatio.text = Math.Round(CharacterS.ThirstIncreaseRatio, 2).ToString();

    }

    private void OnEnable()
    {
        RefreshStats();
    }

    public void ShowDescription(BaseStats stats)
    {
        StatsDescription.text = StatsDescriptions[stats];
    }

}
