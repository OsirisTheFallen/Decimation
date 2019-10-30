using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Tools
{
    internal class GildedSickle : DecimationTool
    {
        protected override string ItemName => "The Gilded Sickle";
        protected override string ItemTooltip => "Allows the collection of hay from grass";
        protected override bool IsClone => true;
        protected override int MeleeDamages => 10;

        protected override void InitTool()
        {
            this.item.CloneDefaults(ItemID.Sickle);

            width = 16;
            height = 16;
            value = Item.buyPrice(0, 0, 1);
            this.item.knockBack = 5;
            useTime = 14;
            useAnimation = 14;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.Anvils);

            recipe.AddIngredient(ItemID.Sickle);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(null, "SoulofTime", 10);

            return recipe;
        }
    }
}