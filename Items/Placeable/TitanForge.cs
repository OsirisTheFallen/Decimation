using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    class TitanForge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titan Forge");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = 0;
            item.rare = 1;
            item.createTile = mod.TileType("TitanForge");
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
        }

        public override void AddRecipes()
        {
            // Adamantite
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteForge);
            recipe.AddIngredient(ItemID.Autohammer);
            recipe.AddIngredient(ItemID.AdamantiteBar, 5);
            recipe.AddIngredient(ItemID.TitaniumBar, 5);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(mod.ItemType("SoulofSpite"), 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddTile(mod.TileType("ChlorophyteAnvil"));
            recipe.SetResult(this);
            recipe.AddRecipe();

            // Titanium
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumForge);
            recipe.AddIngredient(ItemID.Autohammer);
            recipe.AddIngredient(ItemID.AdamantiteBar, 5);
            recipe.AddIngredient(ItemID.TitaniumBar, 5);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(mod.ItemType("SoulofSpite"), 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddTile(mod.TileType("ChlorophyteAnvil"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
