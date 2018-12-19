using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Potions
{
    public class WarlockPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WarlockPotion");
            Tooltip.SetDefault("Give Warlock buff for 10 minutes.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.value = 8000;
            item.rare = 1;
            item.consumable = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.useStyle = 2;
            item.buffType = mod.BuffType("Warlock");
            item.buffTime = 36000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaRegenerationPotion);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.BottledHoney);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}