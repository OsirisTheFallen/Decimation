using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items
{
    public class DuneWormTreasureBag : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("Right click to open");
		}
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 11;
            bossBagNPC = mod.NPCType("AncientDuneWormHead");
            item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {                                         
            player.QuickSpawnItem(mod.ItemType("SoulOfTime"), Main.rand.Next(20, 35));
			player.QuickSpawnItem(ItemID.FossilOre, Main.rand.Next(10, 15));		
			player.QuickSpawnItem(mod.ItemType("TheHourGlass"));
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("DuneWormMask"));
			}
        }
    }
}