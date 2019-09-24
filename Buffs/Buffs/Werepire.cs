using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
   internal class Werepire : DecimationBuff
    {
        protected override string DisplayName => "Werepire!";
        protected override string Description => "Grants both Vampire! and Werewolf effects";

        protected override void Init()
        {
            save = true;
            clearable = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.Werewolf, 1);
            player.AddBuff(mod.BuffType<Vampire>(), 1);

            player.wereWolf = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.AddBuff(BuffID.Werewolf, 1);
            npc.AddBuff(mod.BuffType<Vampire>(), 1);
        }
    }
}
