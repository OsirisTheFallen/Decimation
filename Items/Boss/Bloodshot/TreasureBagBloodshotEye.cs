using System;
using System.IO;
using Decimation.Items.Accessories;
using Decimation.Items.Misc;
using Decimation.Items.Weapons;
using Decimation.Items.Weapons.Bloodshot;
using Decimation.NPCs.Bloodshot;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Bloodshot
{
    public class TreasureBagBloodshotEye : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 11;
            bossBagNPC = mod.NPCType<BloodshotEye>();
            item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {                                         
            player.QuickSpawnItem(mod.ItemType<BloodiedEssence>(), Main.rand.Next(35, 51));	
			player.QuickSpawnItem(mod.ItemType<NecrosisStone>());

            int random = Main.rand.Next(3);
            int weapon = 0;

            switch (random)
            {
                case 0:
                    weapon = mod.ItemType<VampiricShiv>();
                    break;
                case 1:
                    weapon = mod.ItemType<Umbra>();
                    break;
                case 2:
                    weapon = mod.ItemType<BloodStream>();
                    break;
                default:
                    Main.NewText("Unexpected error in Bloodshot Eye drops: weapon drop random is out of range (" + random + ").", Color.Red);
                    break;
            }
        }
    }
}