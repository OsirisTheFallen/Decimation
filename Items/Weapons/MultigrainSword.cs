using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    public class MultigrainSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Multigrain Sword");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 36;
            item.height = 37;
            item.useTime = 26;
            item.useAnimation = 26;     
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = 4000;        
            item.rare = 2;
			item.crit = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
			item.expert = false;
            item.shoot = mod.ProjectileType("StingerM");
			item.shootSpeed = 10f;
		}	
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
			recipe.AddIngredient(ItemID.CactusSword, 1);
			recipe.AddIngredient(ItemID.Pumpkin, 15);
			recipe.AddIngredient(ItemID.Acorn, 5);
			recipe.AddIngredient(ItemID.Hay, 15);
			recipe.AddIngredient(null, "TheGreatwoodSword", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
		}
    }
}