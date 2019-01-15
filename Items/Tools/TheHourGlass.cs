using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Decimation.Items.Tools
{
    public class TheHourGlass : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Hour Glass");
			Tooltip.SetDefault("Costs 50 mana.");
		}
        public override void SetDefaults()
        {
            item.noMelee = true;
            item.mana = 50;
            item.width = 22;
            item.height = 36;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 4;
            item.rare = 11;
			item.expert = true;
		}
		public override bool UseItem(Player player)
		{
			if (Main.dayTime)
			{
				Main.dayTime = false;
			}
			else if (!Main.dayTime)
			{
				Main.dayTime = true;
			}
			return true;
		}
		
	}
}