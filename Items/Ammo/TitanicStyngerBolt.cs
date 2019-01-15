using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ammo
{
    class TitanicStyngerBolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Explodes into deadly shrapnel.");
        }

        public override void SetDefaults()
        {
            item.damage = 35;
            item.knockBack = 2;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 1000;
            item.rare = 3;
            item.shoot = mod.ProjectileType("TitanicStyngerBolt");
            item.shootSpeed = 2;
            item.ammo = AmmoID.StyngerBolt;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.NextBool(100))
                target.AddBuff(mod.BuffType("Amnesia"), 600);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(100))
                target.AddBuff(mod.BuffType("Amnesia"), 600);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StyngerBolt, 50);
            recipe.AddIngredient(mod.ItemType("TitaniteBar"), 3);
            recipe.AddTile(mod.TileType("TitanForge"));
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
