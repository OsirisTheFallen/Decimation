using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    class EndlessPouchofLife : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Pouch of Life");
            Tooltip.SetDefault("cancels ammunition consumption." +
                "\nIncrease maximum life by 15" +
                "\nChange ammunitions in Chlorophyte Bullets" +
                "\n\nProvide a life regeneration boost" +
                "\n+8% ranged damages" +
                "\n+15% chances of critical stike on renged damages");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
            item.accessory = true;
            item.maxStack = 1;
            item.value = 500000;
            item.rare = 7;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 5;
            player.rangedDamage += 0.08f;
            player.rangedCrit += 5;
            player.statLifeMax2 += 15;
            Main.LocalPlayer.GetModPlayer<DecimationPlayer>().endlessPouchofLifeEquipped = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.AddIngredient(null, "EnergyFocuser");
            recipe.AddIngredient(ItemID.EndlessMusketPouch);
            recipe.AddIngredient(ItemID.SoulofSight, 50);
            recipe.AddTile(null, "ChlorophyteAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
