using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Potions
{
    public class CommanderPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Commander Potion");
            Tooltip.SetDefault("Increases your max number of minions by 1 \nMinions damages +10% \nMinions Knockback +10%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.value = 10000;
            item.rare = 1;
            item.consumable = true;
            item.useAnimation = 17;
            item.useTime = 17;
            //item.potion = true;
            item.useTurn = true;
            item.useStyle = 2;
            item.buffType = mod.BuffType("Commander");
            item.buffTime = 36000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SummoningPotion);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(ItemID.VariegatedLardfish);
            recipe.AddIngredient(ItemID.MagicPowerPotion, 2);
            recipe.AddIngredient(ItemID.RottenChunk, 2);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
