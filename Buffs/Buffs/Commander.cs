using Terraria;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
	internal class Commander : DecimationBuff
	{
        protected override string DisplayName => "Commander";
        protected override string Description => "Max minions +1 \nMinion's damages +10% \nMinion's Knockback +10%";

        protected override void Init()
        {
            save = true;
            clearable = true;
        }

        public override void Update(Player player, ref int buffIndex)
		{
			player.maxMinions++;
			player.minionDamage += 0.1f;
			player.minionKB += 0.1f;
		}
    }
}
