using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs
{
    class Hyperthermic : ModBuff
    {
        public override void SetDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Hyperthermic!");
            Description.SetDefault("Water, water everywhere but not a drop to drink... \nBlock mana potions use \nLowers defense by 10% \nLowers speed by 5%");
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense = (int)(player.statDefense * 0.90f);
            player.moveSpeed *= 0.95f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense = (int)(npc.defense * 0.90f);
            npc.velocity *= 0.95f;
        }
    }

    public class HyperthermicManaBlock : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.HasBuff(mod.BuffType("Hyperthermic")) && item.healMana > 0)
                return false;
            else
                return true;
        }
    }
}
