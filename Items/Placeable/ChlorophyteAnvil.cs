using Decimation.Items.Misc.Souls;
using Decimation.Core.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    internal class ChlorophyteAnvil : DecimationPlaceableItem
    {
        protected override string ItemName => "Chlorophyte Anvil";
        protected override string ItemTooltip => "It reacts to the light.";
        protected override int Tile => ModContent.TileType<Tiles.ChlorophyteAnvil>();

        protected override void InitPlaceable()
        {
            width = 20;
            height = 20;
            this.item.maxStack = 1;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.AdamantiteForge);

            recipe.AddIngredient(ModContent.TileType<Tiles.EnchantedAnvil>());
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.Vine, 5);
            recipe.AddIngredient(ItemID.JungleSpores, 16);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ModContent.ItemType<SoulofLife>(), 5);
            recipe.AddIngredient(ModContent.ItemType<SoulofTime>(), 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);

            return recipe;
        }
    }
}