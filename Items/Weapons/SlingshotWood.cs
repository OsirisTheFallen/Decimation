using Decimation.Items.Ammo;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class SlingshotWood : DecimationWeapon
    {
        protected override string ItemName => "Slingshot";
        protected override string ItemTooltip => "Uses pebbles and marbles as ammo";
        protected override DamageType DamagesType => DamageType.THROW;
        protected override int Damages => 7;

        protected override void InitWeapon()
        {
            this.item.noMelee = true;
            width = 32;
            height = 32;
            useTime = 16;
            useAnimation = 16;
            useStyle = 5;
            this.item.shoot = 1;
            item.useAmmo = ModContent.ItemType<Pebble>();
            knockBack = 6;
            rarity = Rarity.Orange;
            useSound = SoundID.Item5;
            shootSpeed = 10f;
            criticalStrikeChance = 10;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.WorkBenches);

            recipe.AddIngredient(ItemID.Wood, 12);
            //			recipe.AddIngredient(null,"CrimsoniteBar", 10);

            return recipe;
        }
    }
}