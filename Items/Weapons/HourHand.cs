// Coded by Evie, Sprite using HourHand.png
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;

namespace Decimation.Items
{
	public class HourHand : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Hour Hand");
			Tooltip.SetDefault("The sands of time flow in your favor!");
		}

		public override void SetDefaults() 
		{
			item.damage = 45;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 28;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = ProjectileType<Projectiles.HourHandProj>();
			item.crit = 7;
			item.shootSpeed = 10f;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)  {
			

			Random random = new Random();
			int choose = random.Next(100);
			if (choose >= 50) {
				player.AddBuff(3, 1500, false);
			} 

			return true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox) {
			Lighting.AddLight(item.Center, 0f, 0f, 1f);
		}
	}
}