using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    public class RangersPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ranger's Pouch");
            Tooltip.SetDefault("25% Chance to not consume ammo \n+10% Ranged damage \n+5% Ranged critical chance");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 10;
            item.rare = 2;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EndlessMusketPouch);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.10f;
            player.rangedCrit += 5;
            player.ammoCost75 = true;
        }

    }
}