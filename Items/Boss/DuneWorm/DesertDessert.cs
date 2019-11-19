using System.Collections.Generic;
using Decimation.NPCs.AncientDuneWorm;
using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.DuneWorm
{
    internal class DesertDessert : DecimationItem
    {
        protected override string ItemName => "Desert Dessert";
        protected override string ItemTooltip => "Summons the Ancient Dune Worm";

        protected override void Init()
        {
            width = 32;
            height = 32;
            value = Item.buyPrice(0, 0, 13);
            useStyle = 4;
            useAnimation = 30;
            useTime = 30;
            consumable = true;

            this.item.maxStack = 20;
        }

        public override bool UseItem(Player player)
        {
            if (player.ZoneDesert)
            {
                if (Main.netMode == 0)
                {
                    Main.PlaySound(15, (int) player.position.X, (int) player.position.Y, 0);
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AncientDuneWormHead>());
                    return true;
                }

                ModPacket packet = this.mod.GetPacket();
                packet.Write((byte) DecimationModMessageType.SpawnBoss);
                packet.Write(ModContent.NPCType<AncientDuneWormHead>());
                packet.Write(player.whoAmI);
                packet.Send();
            }

            return false;
        }

        protected override ModRecipe GetRecipe() //How to craft this item
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.Anvils});

            recipe.AddIngredient(ItemID.SandBlock, 10);
            recipe.AddIngredient(ItemID.WormTooth, 5);

            return recipe;
        }
    }
}