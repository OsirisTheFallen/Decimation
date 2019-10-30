using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;

namespace Decimation.Items.Weapons.Arachnus
{
    internal class GlaiveWeaver : DecimationWeapon
    {
        protected override string ItemName => "Glaive Weaver";
        protected override string ItemTooltip => "Your palm burns as you wield this godly weapon.";
        protected override int Damages => 850;
        protected override string Projectile => "ArchingSolarBlade";

        protected override void InitWeapon()
        {
            width = 42;
            height = 46;
            value = Item.buyPrice(0, 45);
            rarity = Rarity.Red;
            useTime = 15;
            useAnimation = 15;
            shootSpeed = 15;
            criticalStrikeChance = 10;
            autoReuse = true;
            knockBack = 4;
        }
    }
}