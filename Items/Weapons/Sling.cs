using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria.ID;

namespace Decimation.Items.Weapons
{
    internal class Sling : DecimationWeapon
    {
        protected override string ItemName => "Sling";
        protected override string ItemTooltip => "Uses pebbles and marbles as ammo";
        protected override DamageType DamagesType => DamageType.RANGED;
        protected override int Damages => 9;
        protected override string Ammo => "Pebble";

        protected override void InitWeapon()
        {
            width = 30;
            height = 22;
            useTime = 16;
            useAnimation = 16;
            knockBack = 6;
            rarity = Rarity.Orange;
            useSound = SoundID.Item5;
            shootSpeed = 10f;
            criticalStrikeChance = 10;
        }
    }
}