using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace Decimation.Items.Weapons
{
    public class Sling : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sling");
			Tooltip.SetDefault("Uses pebbles and marbles as ammo");
		}
        public override void SetDefaults()
        {
            item.damage = 9; 
            item.noMelee = true; 
            item.ranged = true; 
            item.width = 30; 
            item.height = 22; 
            item.useTime = 16; 
            item.useAnimation = 16;  
            item.useStyle = 1; 
            item.shoot = 1; 
			item.useAmmo = mod.ItemType("Pebble");
            item.knockBack = 6; 
            item.rare = 3; 
            item.UseSound = SoundID.Item5;
            item.autoReuse = false; 
            item.shootSpeed = 10f;
            item.crit = 10; 
		}	
	}
}