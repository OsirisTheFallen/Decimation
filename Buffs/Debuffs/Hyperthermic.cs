using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
    internal class Hyperthermic : DecimationBuff
    {
        protected override string DisplayName => "Hyperthermic!";
        protected override string Description => "Water, water everywhere but not a drop to drink... \nBlock mana potions use \nLowers defense by 10% \nLowers speed by 5%";
        public override bool Debuff => true;

        protected override void Init()
        {
            save = false;
            clearable = false;
            displayTime = true;
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
            return !(player.HasBuff(mod.BuffType("Hyperthermic")) && item.healMana > 0);
        }
    }
}
