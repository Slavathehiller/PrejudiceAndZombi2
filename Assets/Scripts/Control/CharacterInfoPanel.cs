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
    Perception = 6
}

public class CharacterInfoPanel : MonoBehaviour
{

    private Dictionary<BaseStats, string> PrimaryStatsDescriptions = new Dictionary<BaseStats, string>();

    [SerializeField] private Text PrimaryStatsDescription;

    [SerializeField] private Image Portrait;
    [SerializeField] private Text Name;

    [SerializeField] private Text Strength;
    [SerializeField] private Text Dexterity;
    [SerializeField] private Text Agility;
    [SerializeField] private Text Constitution;
    [SerializeField] private Text Intellect;
    [SerializeField] private Text Concentration;
    [SerializeField] private Text Perception;

    [SerializeField] private GameController gameController;

    private ICharacter Character;

    private void Awake()
    {
        Character = gameController.character;
        PrimaryStatsDescriptions.Add(BaseStats.Strength, "���������� ���� ���������. ������ �� ��������� ���� � ������� ��� � ������������ ����������� ���.");
        PrimaryStatsDescriptions.Add(BaseStats.Dexterity, "����������� �������� � �������� ������� ���������. ������ �� ������ ������, �������� �������������� �� � ����������.");
        PrimaryStatsDescriptions.Add(BaseStats.Agility, "�������� � ���������� ���������. ������ �� ���������� � ��� � �������� �������������� ��");
        PrimaryStatsDescriptions.Add(BaseStats.Constitution, "���������� �������� ���������. ������ �� ������������ ���������� � �������� �������������� �������� � ��������.");
        PrimaryStatsDescriptions.Add(BaseStats.Intellect, "���������� ����������� � ���������������� ���������. ������ �� ������ �������� �������� � ���������� ������, � ��� �� �� ���� ������� ����������� ���� � ���.");
        PrimaryStatsDescriptions.Add(BaseStats.Concentration, "����������� ��������� ����������������� �� ������. ������ �� ������������ ���������� �� � ������ �������������� ���.");
        PrimaryStatsDescriptions.Add(BaseStats.Perception, "������� ������ � ���������������� ���������. ������ �� ������ �������� �������� � ������������� ���.");

    }

    public void RefreshStats()
    {
        Name.text = Character.Name;
        Portrait.sprite = Character.Portrait;

        Strength.text = Character.Stats.inStrength.ToString();
        Dexterity.text = Character.Stats.inDexterity.ToString();
        Agility.text = Character.Stats.inAgility.ToString();
        Constitution.text = Character.Stats.inConstitution.ToString();
        Intellect.text = Character.Stats.inIntellect.ToString();
        Concentration.text = Character.Stats.inConcentration.ToString();
        Perception.text = Character.Stats.inPerception.ToString();
    }

    private void OnEnable()
    {
        RefreshStats();
    }

    public void ShowDescription(BaseStats stats)
    {
        PrimaryStatsDescription.text = PrimaryStatsDescriptions[stats];
    }

}
