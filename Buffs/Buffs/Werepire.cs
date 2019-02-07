using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
    class Werepire : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Werepire!");
            Description.SetDefault("Grants both Vampire! and Werewolf effects");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
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
