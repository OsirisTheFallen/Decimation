using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items
{
    public class TreasureBagBloodshotEye : ModItem
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
            bossBagNPC = mod.NPCType("TheBloodshotEye");
            item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {                                         
            player.QuickSpawnItem(mod.ItemType("BloodiedEssence"), Main.rand.Next(35, 40));	
			player.QuickSpawnItem(mod.ItemType("NecrosisStone"));
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("VampiricShiv"));				
			}
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("Umbra"));
			}
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("BloodStream"));				
			}			
        }
    }
}