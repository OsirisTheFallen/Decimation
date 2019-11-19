using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ammo
{
    internal class Pebble : DecimationAmmo
    {
        protected override string ItemName => "Pebble";
        protected override string ItemTooltip => "For use with slings and slingshots";
        protected override string Projectile => "Pebble";
        protected override int Ammo => ModContent.ItemType<Pebble>();

        protected override void InitAmmo()
        {
            damages = 11;
            width = 16;
            height = 16;
            this.item.maxStack = 999;
            this.item.consumable = true;
            projKnockBack = 1f;
            this.item.value = Item.sellPrice(0, 0, 1);
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 15, TileID.WorkBenches);

            recipe.AddIngredient(ItemID.StoneBlock);

            return recipe;
        }
    }
}