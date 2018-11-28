using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Arachnus
{
    class MoltenArachnidsAmulet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Arachnid's Amulet");
            Tooltip.SetDefault("Summon Arachnus");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 450000;
            item.rare = 10;
            item.maxStack = 1;
            item.useStyle = 4;
            item.useAnimation = 30;
            item.useTime = 30;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 3);
            recipe.AddIngredient(mod.ItemType("Thermoplasm"));
            recipe.AddTile(mod.TileType("TitanForge"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
