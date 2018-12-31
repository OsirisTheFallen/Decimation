using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Projectiles;
using Decimation.Items.Misc.Souls;
using Decimation.Tiles;
using Microsoft.Xna.Framework;

namespace Decimation.Items.Weapons
{
    class RasGaze : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ra's Gaze");
            Tooltip.SetDefault("Lay down the wrath of Ra!\nWIP");
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 46;
            item.magic = true;
            item.mana = 15;
            item.damage = 0;
            item.noMelee = true;
            item.knockBack = 4;
            item.crit = 8;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType<RasGazeLaser>();
            item.shootSpeed = 1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeetleHusk, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 20);
            recipe.AddIngredient(mod.ItemType<SoulofLife>(), 10);
            recipe.AddTile(mod.TileType<ChlorophyteAnvil>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
