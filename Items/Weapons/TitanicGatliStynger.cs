using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    class TitanicGatliStynger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Feel the rage of Kronos by your side.");
        }

        public override void SetDefaults()
        {
            item.width = 52;
            item.height = 26;
            item.CloneDefaults(ItemID.Stynger);
            item.damage = 950;
            item.crit = 8;
            item.knockBack = 11;
            item.useTime = 10;
            item.useAnimation = 10;
            item.rare = 10;
            item.value = 600000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChainStynger");
            recipe.AddIngredient(null, "TitaniteBar", 15);
            // recipe.AddIngredient(null, "CondensedMight", 5);
            recipe.AddIngredient(null, "DenziumBar");
            recipe.AddTile(null, "TitanForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType<Projectiles.TitanicStyngerBolt>();
            return true;
        }
    }
}
