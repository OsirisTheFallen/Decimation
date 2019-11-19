using Decimation.Buffs.Buffs;
using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Potions
{
    internal class CommanderPotion : DecimationPotion
    {
        protected override string ItemName => "Commander Potion";

        protected override string ItemTooltip =>
            "Increases your max number of minions by 1 \nMinions damages +10% \nMinions knockback +10%";

        protected override int BuffType => ModContent.BuffType<Commander>();
        protected override int BuffTime => 36000;

        protected override void InitPotion()
        {
            value = Item.buyPrice(0, 1);
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.AlchemyTable);

            recipe.AddIngredient(ItemID.SummoningPotion);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(ItemID.VariegatedLardfish);
            recipe.AddIngredient(ItemID.MagicPowerPotion, 2);
            recipe.AddIngredient(ItemID.RottenChunk, 2);

            return recipe;
        }
    }
}