namespace Assets.Scripts.Weapon
{
    public class HomemadeSMG : RangedWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Name = "Самодельный пистолет-пулемёт";
            type = WeaponType.SMG;
            ShootCost = 4;
            prefab = prefabsController.homemade_SMG;
            Description = "Пистолет-пулемет, сделаный из подручных материалов. Меткость и убойная сила оставляет желать лучшего.";
        }

        protected override void Update()
        {
            base.Update();
        }

    }
}
