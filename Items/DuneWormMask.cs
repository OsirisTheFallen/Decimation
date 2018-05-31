using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Decimation.Items
{
	[AutoloadEquip(EquipType.Head)]	
    public class DuneWormMask : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Dune Worm Mask");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.rare = 3;
			item.vanity = true;
		}

		public override bool DrawHead()
		{
			return false;
		}
	}
}