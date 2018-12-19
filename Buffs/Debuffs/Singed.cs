using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
    class Singed : ModBuff
    {
        public override void SetDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Singed!");
            Description.SetDefault("Burn target \nSlow target by 5% \nLower target defense by 5% \n Block potion use");
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.onFire2 = true;
            player.moveSpeed *= 0.95f;
            player.statDefense = (int)(0.95f * player.statDefense);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.onFire = true;
            npc.velocity *= 0.95f;
            npc.defense = (int)(0.95f * npc.defense);
        }
    }

    public class SingedGlobalItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.HasBuff(mod.BuffType("Singed")))
            {
                return !(item.UseSound != null && item.useStyle == 2);
            }
            return true;
        }
    }
}