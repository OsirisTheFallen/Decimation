using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    class MoltenStyngerBolt : ModItem
    {
        public override void AutoStaticDefaults()
        {
            Tooltip.SetDefault("Explodes into molten shrapnel.");
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.knockBack = 1;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 1000;
            item.rare = 3;
            item.shoot = mod.ProjectileType("MoltenStyngerBolt");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.StyngerBolt;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Singed"), 600);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("Singed"), 600);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StyngerBolt, 50);
            recipe.AddIngredient(mod.ItemType("Thermoplasm"), 5);
            recipe.AddTile(mod.TileType("TitanForge"));
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
