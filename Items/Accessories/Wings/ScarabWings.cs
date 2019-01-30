using Decimation.Items.Misc.CondensedSouls;
using Decimation.Items.Ores;
using Decimation.Projectiles;
using Decimation.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    class ScarabWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Blessed by the sun");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = 50000;
            item.rare = 10;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 240;
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1.05f, 0.95f, 0.55f);

            if (player.wingTime % 2 == 1)
                Projectile.NewProjectile(player.Center, new Microsoft.Xna.Framework.Vector2(0, 0), mod.ProjectileType<Ember>(), 25, 5, player.whoAmI);
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 9f;
            acceleration *= 2.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeetleWings);
            recipe.AddIngredient(ItemID.WingsSolar);
            recipe.AddIngredient(mod.ItemType<CondensedSpite>(), 2);
            recipe.AddIngredient(mod.ItemType<DenziumBar>(), 5);
            recipe.AddTile(mod.TileType<TitanForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
