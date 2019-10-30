using Decimation.Items.Misc;
using Decimation.NPCs.Bloodshot;
using Decimation.Core.Items;
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

            this.item.maxStack = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime || !NPC.AnyNPCs(this.mod.NPCType<BloodshotEye>());
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode == 0)
            {
                Main.PlaySound(15, (int) player.position.X, (int) player.position.Y, 0);
                NPC.SpawnOnPlayer(player.whoAmI, this.mod.NPCType<BloodshotEye>());
            }
            else
            {
                ModPacket packet = this.mod.GetPacket();
                packet.Write((byte) DecimationModMessageType.SpawnBoss);
                packet.Write(this.mod.NPCType<BloodshotEye>());
                packet.Write(player.whoAmI);
                packet.Send();
            }

            return true;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.DemonAltar);

            recipe.AddIngredient(this.mod.ItemType<BloodiedEssence>(), 10);
            recipe.AddIngredient(ItemID.Lens);

            return recipe;
        }
    }
}