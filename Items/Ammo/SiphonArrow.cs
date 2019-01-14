using Decimation.Items.Misc;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ammo
{
    class SiphonArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Aspires other's life.");
        }

        public override void SetDefaults()
        {
            item.damage = 11;
            item.ranged = true;
            item.width = 14;
            item.height = 32;
            item.maxStack = 999;
            item.rare = 1;
            item.consumable = true;
            item.knockBack = 2;
            item.value = 55;
            item.shoot = mod.ProjectileType<Projectiles.SiphonArrow>();
            item.shootSpeed = 2.5f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenArrow, 50);
            recipe.AddIngredient(mod.ItemType<BloodiedEssence>());
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
