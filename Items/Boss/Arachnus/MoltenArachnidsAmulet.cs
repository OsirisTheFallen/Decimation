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
            item.value = 1300;
            item.rare = 1;
            item.maxStack = 1;
            item.useStyle = 4;
            item.useAnimation = 30;
            item.useTime = 30;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Roar, (int)player.position.X + 500, (int)player.position.Y, 0);
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Arachnus"));
            return true;
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
