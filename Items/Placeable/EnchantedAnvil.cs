using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    class EnchantedAnvil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Anvil");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = 0;
            item.rare = 1;
            item.createTile = mod.TileType("EnchantedAnvil");
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MythrilAnvil, 1);
            recipe.AddIngredient(ItemID.IronAnvil, 1);
            recipe.AddIngredient(mod.ItemType("SoulofLife"), 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MythrilAnvil, 1);
            recipe.AddIngredient(ItemID.LeadAnvil, 1);
            recipe.AddIngredient(mod.ItemType("SoulofLife"), 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.OrichalcumAnvil, 1);
            recipe.AddIngredient(ItemID.IronAnvil, 1);
            recipe.AddIngredient(mod.ItemType("SoulofLife"), 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.OrichalcumAnvil, 1);
            recipe.AddIngredient(ItemID.LeadAnvil, 1);
            recipe.AddIngredient(mod.ItemType("SoulofLife"), 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
