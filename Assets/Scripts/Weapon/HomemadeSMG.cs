namespace Assets.Scripts.Weapon
{
    public class HomemadeSMG : RangedWeapon
    {
        public override string Caliber => "9x18 ��";

        protected override void Awake()
        {
            base.Awake();
            Name = "����������� ��������-������";
            type = WeaponType.SMG;
            ShootCost = 4;
            prefab = prefabsController.homemade_SMG;
        }

        protected override void Update()
        {
            base.Update();
        }

    }
}
