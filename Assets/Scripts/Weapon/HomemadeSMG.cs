namespace Assets.Scripts.Weapon
{
    public class HomemadeSMG : RangedWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Name = "����������� ��������-������";
            type = WeaponType.SMG;
            ShootCost = 4;
            prefab = prefabsController.homemade_SMG;
            Description = "��������-�������, �������� �� ��������� ����������. �������� � ������� ���� ��������� ������ �������.";
        }

        protected override void Update()
        {
            base.Update();
        }

    }
}
