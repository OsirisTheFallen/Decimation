using Terraria;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
	public class Commander : ModBuff
	{
		public override void SetDefaults()
		{
			Main.debuff[Type] = false;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Commander");
			Description.SetDefault("Max minions +1 \nMinions damages +10% \nMinions Knockback +10%");
			canBeCleared = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.maxMinions++;
			player.minionDamage += 0.1f;
			player.minionKB += 0.1f;
		}
    }
}
