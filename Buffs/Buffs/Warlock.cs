using Terraria;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
	public class Warlock : ModBuff
	{
		public override void SetDefaults()
		{
			Main.debuff[Type] = false;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Warlock");
			Description.SetDefault("Max mana +50 \nMana Regeneration Bonus +25 \nMagic Damages + 10% \nMagic Critical Chances + 5%");
			canBeCleared = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statManaMax2 += 50;
			// To review
			player.manaRegenBonus += 25;
			player.magicDamage += 0.1f;
			player.magicCrit += 5;
		}
    }
}