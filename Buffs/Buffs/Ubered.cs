using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
    class Ubered : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ubered!");
            Description.SetDefault("YOU ARE ZE UBERMENSCH!");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += (int)(player.statDefense * 0.5f);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense += (int)(npc.defense * 0.5f);
        }
    }
}
