using Decimation.Items.Misc;
using Decimation.NPCs.Bloodshot;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Bloodshot
{
    class BloodiedMaw : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a mangled eye of a god.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.consumable = true;
            item.rare = 1;
            item.maxStack = 1;
            item.useStyle = 4;
            item.useAnimation = 30;
            item.useTime = 30;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<BloodiedEssence>(), 10);
            recipe.AddIngredient(ItemID.Lens);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
