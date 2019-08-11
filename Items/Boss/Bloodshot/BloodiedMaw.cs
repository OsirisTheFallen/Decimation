using Decimation.Items.Misc;
using Decimation.NPCs.Bloodshot;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Bloodshot
{
    internal class BloodiedMaw : DecimationItem
    {
        protected override string ItemName => "Bloodied Maw";
        protected override string ItemTooltip => "{$CommonItemTooltip.RightClickToOpen}";

        protected override void Init()
        {
            width = 20;
            height = 24;
            consumable = true;
            useStyle = 4;
            useAnimation = 30;
            useTime = 30;

            item.maxStack = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime || !NPC.AnyNPCs(mod.NPCType<BloodshotEye>());
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode == 0)
            {
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType<BloodshotEye>());
            }
            else
            {
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)DecimationModMessageType.SpawnBoss);
                packet.Write(mod.NPCType<BloodshotEye>());
                packet.Write(player.whoAmI);
                packet.Send();
            }
            return true;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.DemonAltar);

            recipe.AddIngredient(mod.ItemType<BloodiedEssence>(), 10);
            recipe.AddIngredient(ItemID.Lens);

            return recipe;
        }
    }
}
