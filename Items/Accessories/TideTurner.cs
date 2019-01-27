using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Items.Ores;
using Decimation.Tiles;

namespace Decimation.Items.Accessories
{
    class TideTurner : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Not one of ya’s going to survive this!\n" +
                "+3 defense\n" +
                "Deals the same amount of damage as held item\n" +
                "Increase underwater mobility\n" +
                "+10% chance to dodge attacks when charging");
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 36;
            item.accessory = true;
            item.value = 30000;
            item.rare = -12;
            item.melee = true;
            item.damage = 0;
            item.defense = 3;
            item.shieldSlot = 5;
            item.crit = 1;
            item.knockBack = 9;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();

            item.damage = player.HeldItem.damage;
            modPlayer.dashDamages = player.HeldItem.damage;
            modPlayer.dash = 2;

            player.accFlipper = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EoCShield);
            recipe.AddIngredient(ItemID.Coral, 10);
            recipe.AddIngredient(mod.ItemType<DenziumBar>(), 5);
            recipe.AddTile(mod.TileType<ChlorophyteAnvil>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
