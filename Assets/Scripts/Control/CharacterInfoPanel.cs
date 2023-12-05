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
        StatsDescriptions.Add(BaseStats.Strength, "���������� ���� ���������. ������ �� ��������� ���� � ������� ��� � ������������ ����������� ���.");
        StatsDescriptions.Add(BaseStats.Dexterity, "����������� �������� � �������� ������� ���������. ������ �� ������ ������, �������� �������������� �� � ����������.");
        StatsDescriptions.Add(BaseStats.Agility, "�������� � ���������� ���������. ������ �� ���������� � ��� � �������� �������������� ��");
        StatsDescriptions.Add(BaseStats.Constitution, "���������� �������� ���������. ������ �� ������������ ���������� � �������� �������������� �������� � ��������.");
        StatsDescriptions.Add(BaseStats.Intellect, "���������� ����������� � ���������������� ���������. ������ �� ������ �������� �������� � ���������� ������, � ��� �� �� ���� ������� ����������� ���� � ���.");
        StatsDescriptions.Add(BaseStats.Concentration, "����������� ��������� ����������������� �� ������. ������ �� ������������ ���������� �� � ������ �������������� ���.");
        StatsDescriptions.Add(BaseStats.Perception, "������� ������ � ���������������� ���������. ������ �� ������ �������� �������� � ������������� ���.");
        StatsDescriptions.Add(BaseStats.MeleeAbility, "����������� ������� � ������� ���. ������� �� �������� � �����������.");
        StatsDescriptions.Add(BaseStats.MeleeCritChance, "���� ������� ����������� ���� � ������� ���. ������� �� �������� � ����������.");
        StatsDescriptions.Add(BaseStats.RangedAbility, "������ ������������ ������� �������� ���. ������� �� ����������, �������� � ������������.");
        StatsDescriptions.Add(BaseStats.RangedCritChance, "���� ������� ����������� ���� � ������� ���. ������� �� ����������, ���������� � ������������.");
        StatsDescriptions.Add(BaseStats.PureMeleeDamage, "���� ��������� ������ ������. ������� �� ����.");
        StatsDescriptions.Add(BaseStats.MaxActionPoint, "������������ ���������� ��. ������� �� ������������ � �����������.");
        StatsDescriptions.Add(BaseStats.IncomeActionPoint, "���������� ��, ������� ����������������� �� ���. ������� �� ����������� � ��������.");
        StatsDescriptions.Add(BaseStats.MaxHealth, "������������ ���������� ��������. ������� �� ������������.");
        StatsDescriptions.Add(BaseStats.Initiative, "������ �� ���������� ����. ������� �� �����������.");
        StatsDescriptions.Add(BaseStats.Stealth, "����������� ���������� �� ���������. ������� �� ��������, ���������� � ���������� ");
        StatsDescriptions.Add(BaseStats.Observation, "����������� �������� ��������. ������� �� ���������� � ���������� ");
        StatsDescriptions.Add(BaseStats.HealthRestoreRatio, "�������� �������������� ��������. ������� �� ������������");
        StatsDescriptions.Add(BaseStats.EnergyRestoreRatio, "�������� �������������� ��������. ������� �� ������������");
        StatsDescriptions.Add(BaseStats.HungerIncreaseRatio, "�������� ���������� ������. ������� �� ������������");
        StatsDescriptions.Add(BaseStats.ThirstIncreaseRatio, "�������� ���������� �����. ������� �� ������������");


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
