using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    class ChlorophyteAnvil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorophyte Anvil");
            Tooltip.SetDefault("It reacts to the light.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = 0;
            item.rare = 1;
            item.createTile = mod.TileType("ChlorophyteAnvil");
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.TileType("EnchantedAnvil"));
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.Vine, 5);
            recipe.AddIngredient(ItemID.JungleSpores, 16);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(mod.ItemType("SoulofLife"), 5);
            recipe.AddIngredient(mod.ItemType("SoulofTime"), 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
