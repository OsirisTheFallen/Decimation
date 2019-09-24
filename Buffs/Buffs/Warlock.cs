using Terraria;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
	internal class Warlock : DecimationBuff
    {
        protected override string DisplayName => "Warlock";
        protected override string Description => "Max mana +50 \nMana Regeneration Bonus +25 \nMagic Damages + 10% \nMagic Critical Chances + 5%";

        protected override void Init()
		{
			displayTime = true;
            save = true;
            clearable = true;
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