using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Decimation.Buffs
{
	public class Slimed : ModBuff
	{

		private int i = 0;


		public override void SetDefaults()
		{
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Slimed!");
			Description.SetDefault("Movement speed -20% \nDeal 2 damages each 5 seconds \nIncrease jump height \n Block potion use");
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			i++;
			player.moveSpeed -= 0.2f;
			Player.jumpHeight += 15;

			if(i >= 300)
			{
				player.Hurt(PlayerDeathReason.LegacyDefault(), 2, 0);
				i = 0;
			}
		}
	}

    public class SlimedGlobalItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.HasBuff(mod.BuffType("Slimed")))
            {
                return !(item.UseSound != null && item.useStyle == 2);
            }
            return true;
        }
    }
}