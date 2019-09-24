using System;
using Decimation.Buffs.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Potions
{
    internal class WarlockPotion : DecimationPotion
    {
        protected override string ItemName => "Warlock Potion";
        protected override string ItemTooltip => "Increased Mana Regeneration \nIncreased max mana \n10% increased magic damage";
        protected override int BuffType => mod.BuffType<Warlock>();
        protected override int BuffTime => 36000;

        protected override void InitPotion()
        {
            value = Item.buyPrice(0, 0, 80);
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.AlchemyTable);

            recipe.AddIngredient(ItemID.ManaRegenerationPotion);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.BottledHoney);

            return recipe;
        }
    }
}