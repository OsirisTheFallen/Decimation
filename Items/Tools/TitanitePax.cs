using Decimation.Items.Ores;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Tools
{
    internal class TitanitePax : DecimationTool
    {
        protected override string ItemName => "Titanite Pax";
        protected override int MeleeDamages => 64;
        protected override int PickaxePower => 250;
        protected override int AxePower => 27;

        protected override void InitTool()
        {
            width = 48;
            height = 52;
            this.item.crit = 14;
            useTime = 5;
            useAnimation = 15;
            this.item.knockBack = 7;
            rarity = Rarity.LightRed;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, ModContent.TileType<TitanForge>());

            recipe.AddIngredient(ModContent.ItemType<TitaniteBar>(), 12);
            recipe.AddIngredient(ItemID.SoulofMight, 15);

            return recipe;
        }
    }
}