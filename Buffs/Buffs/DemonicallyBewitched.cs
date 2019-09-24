using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
    internal class DemonicallyBewitched : DecimationBuff
    {
        protected override string DisplayName => "Demonically Bewitched";
        protected override string Description => "+1 minion" + "\nIncrease minions damages and knockback";

        protected override void Init()
        {
            save = true;
            clearable = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions += 1;
            player.minionDamage *= 1.05f;
            player.minionKB *= 1.02f;
        }
    }
}
