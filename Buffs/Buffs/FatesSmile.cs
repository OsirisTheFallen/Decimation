using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Decimation.Buffs.Buffs
{
    public class FatesSmile : ModBuff
    {
        public override void SetDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Fate's Smile");
            Description.SetDefault("Provide a slight life regeneration boost.");
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 1;
        }
    }
}