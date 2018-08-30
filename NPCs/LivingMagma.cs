using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.NPCs
{
    class LivingMagma : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Magma");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 28;
            npc.aiStyle = 1;
            npc.damage = 50;
            npc.defense = 18;
            npc.lifeMax = 150;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.alpha = 55;
            npc.value = 400f;
            npc.scale = 1.1f;
            npc.buffImmune[24] = true;
            npc.buffImmune[74] = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.8f;
            aiType = NPCID.ToxicSludge;
            animationType = NPCID.ToxicSludge;
        }

        public override void AI()
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire);
            base.AI();
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            Main.LocalPlayer.AddBuff(mod.BuffType("Singed"), 600);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Underworld.Chance;
        }
    }
}
