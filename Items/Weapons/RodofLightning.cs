using Decimation.Core.Items;

namespace Decimation.Items.Weapons
{
    internal class RodofLightning : DecimationWeapon
    {
        protected override string ItemName => "Rod of Lightning";
        protected override int Damages => 90;
        protected override DamageType DamagesType => DamageType.MAGIC;
        protected override string Projectile => "LightningSphere";

        protected override void InitWeapon()
        {
            this.item.mana = 17;
            knockBack = 7;
            criticalStrikeChance = 15;
            useStyle = 3;
            useTime = 15;
            useAnimation = 15;
            shootSpeed = 15f;
        }
    }
}