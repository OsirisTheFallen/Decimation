using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class Pebble : DecimationWeapon
    {
        protected override string ItemName => "Pebble";
        protected override string ItemTooltip => "For use with slings and slingshots";
        protected override DamageType DamagesType => DamageType.RANGED;
        protected override int Damages => 1;
        protected override string Projectile => "Pebble";

        protected override void InitWeapon()
        {
            width = 8;
            height = 8;
            item.maxStack = 999;
            item.consumable = true;
            knockBack = 1f;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.ammo = item.type; // The first item in an ammo class sets the AmmoID to it's type
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 15, TileID.WorkBenches);

            recipe.AddIngredient(ItemID.StoneBlock);

            return recipe;
        }
    }
}