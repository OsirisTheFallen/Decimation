using Decimation.NPCs.AncientDuneWorm;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc.DuneWorm
{

    public class DesertDessert : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Dessert");
            Tooltip.SetDefault("Used in the desert to feed a ravenous creature.");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 1300;
            item.rare = 1;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useAnimation = 30;
            item.useTime = 30;
            item.consumable = true;
        }


        public override bool UseItem(Player player)
        {
            if (player.ZoneDesert)
            {
                if (Main.netMode == 1)
                {
                    ModPacket packet = mod.GetPacket();
                    packet.Write((byte)DecimationModMessageType.SpawnBoss);
                    packet.Write(mod.NPCType<AncientDuneWormHead>());
                    packet.Write(player.whoAmI);
                    packet.Send();
                }
                else if (Main.netMode == 0)
                {
                    Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType<AncientDuneWormHead>());
                }
                return true;
            }

            return false;
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SandBlock, 10);
            recipe.AddIngredient(ItemID.WormTooth, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
