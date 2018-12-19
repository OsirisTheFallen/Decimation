using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Potions
{
    public class Antidote : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antidote");
            Tooltip.SetDefault("Cure poison and venom.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.value = 0;
            item.rare = 1;
            item.consumable = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.useStyle = 2;
        }

        public override bool UseItem(Player player)
        {
            player.ClearBuff(BuffID.Poisoned);
            player.ClearBuff(BuffID.Venom);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Waterleaf);
            recipe.AddIngredient(ItemID.Moonglow);
            recipe.AddIngredient(ItemID.BottledHoney);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}