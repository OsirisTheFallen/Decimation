using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs
{
    class DemonicallyBewitched : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Demonically Bewitched");
            Description.SetDefault("+1 minion" + "\nIncrease minions damages and knockback");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions += 1;
            player.minionDamage *= 1.05f;
            player.minionKB *= 1.02f;
        }
    }
}
