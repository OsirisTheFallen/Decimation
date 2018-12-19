using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Potions
{
    public class EnchantedMushroom : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Mushroom");
            Tooltip.SetDefault("Cure poison \nGive Happy! buff");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.value = 1500;
            item.rare = 1;
            item.consumable = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.potion = true;
            item.useTurn = true;
            item.useStyle = 2;
            item.healLife = 90;
        }

        public override bool UseItem(Player player)
        {
            player.ClearBuff(BuffID.Poisoned);
            player.AddBuff(BuffID.Sunflower, 7200);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Mushroom);
            recipe.AddIngredient(ItemID.GlowingMushroom);
            recipe.AddIngredient(ItemID.BottledHoney);
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}