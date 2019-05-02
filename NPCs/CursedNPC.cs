using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Decimation.NPCs
{
    internal class CursedNPC : GlobalNPC
    {
        // Life bonus in percents
        public static int LifeBonus { get; set; }

        // Damage bonus in percents
        public static int DamageBonus { get; set; }

        // Mana leech in percents
        public static int ManaLeech { get; set; }

        public override void SetDefaults(NPC npc)
        {
            if (!npc.friendly)
                npc.lifeMax = (int)(npc.lifeMax * (1 + LifeBonus / 100f));

            base.SetDefaults(npc);
        }

        public override void AI(NPC npc)
        {
            // Should be moved in future
            if (!Main.LocalPlayer.GetModPlayer<DecimationPlayer>().hasCursedAccessory)
            {
                LifeBonus = 0;
                DamageBonus = 0;
                ManaLeech = 0;
            }

            base.AI(npc);
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (!npc.friendly && target.GetModPlayer<DecimationPlayer>().hasCursedAccessory)
                damage *= (int)(1 + DamageBonus / 100f);
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (!npc.friendly && target.GetModPlayer<DecimationPlayer>().hasCursedAccessory)
                target.statMana *= (int)(1 - ManaLeech / 100f);
        }
    }
}
