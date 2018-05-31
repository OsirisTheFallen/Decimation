using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Decimation.Items
{
    public class DuneWormTrophey : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Dune Worm Trophy");
			Tooltip.SetDefault("WIP");
		}
        public override void SetDefaults()
        {
            item.noMelee = true;
            item.width = 32;
            item.height = 32;
            item.rare = 3;
		}
	}
}