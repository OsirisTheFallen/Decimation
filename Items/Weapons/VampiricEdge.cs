using Decimation.Items.Misc.Souls;
using Decimation.Items.Weapons.Bloodshot;
using Decimation.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    class VampiricEdge : ModItem
    {
        private int shootDelay = 72;
        private int timeToShoot = 72;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 52;
            item.melee = true;
            item.damage = 54;
            item.crit = 6;
            item.knockBack = 4.5f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType<Tooth>();
            item.shootSpeed = 5f;
            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 2;
            item.useStyle = 1;
        }

        public override void UpdateInventory(Player player)
        {
            if (timeToShoot > 0) timeToShoot--;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (timeToShoot > 0) return false;

            timeToShoot = shootDelay;

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BloodButcherer);
            recipe.AddIngredient(mod.ItemType<VampiricShiv>());
            recipe.AddIngredient(mod.ItemType<SoulofTime>(), 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LightsBane);
            recipe.AddIngredient(mod.ItemType<VampiricShiv>());
            recipe.AddIngredient(mod.ItemType<SoulofTime>(), 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
