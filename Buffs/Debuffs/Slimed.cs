using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Decimation.Buffs.Debuffs
{
    internal class Slimed : DecimationBuff
    {
        protected override string DisplayName => "Slimed!";
        protected override string Description => "Movement speed -20% \nDeal 2 damages each 5 seconds \nIncrease jump height \n Block potion use";
        public override bool Debuff => true;

        protected override void Init()
        {
            save = false;
            displayTime = true;
            clearable = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 0.80f;
            player.jumpBoost = true;

            if (player.miscCounter % 300 == 0)
                player.Hurt(PlayerDeathReason.LegacyDefault(), 2, 0);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= 0.80f;
            npc.lifeRegen -= 1;
        }
    }

    public class SlimedGlobalItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.HasBuff(mod.BuffType("Slimed")))
            {
                return !(item.UseSound != null && item.useStyle == 2);
            }
            return true;
        }
    }
}