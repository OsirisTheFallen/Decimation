using Decimation.Buffs.Buffs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    class DraculaPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Dont tread under the suns gaze!\n\n" +
                "Gives Vampire debuff\n" +
                "Bats will be friendly\n" +
                "You will burn at sun"
                );
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 62;
            item.accessory = true;
            item.maxStack = 1;
            item.rare = 6;
            item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(mod.BuffType<Vampire>(), 1);

            player.npcTypeNoAggro[NPCID.CaveBat] = true;
            player.npcTypeNoAggro[NPCID.JungleBat] = true;
            player.npcTypeNoAggro[NPCID.Hellbat] = true;
            player.npcTypeNoAggro[NPCID.IceBat] = true;
            player.npcTypeNoAggro[NPCID.GiantBat] = true;
            player.npcTypeNoAggro[NPCID.IlluminantBat] = true;
            player.npcTypeNoAggro[NPCID.Lavabat] = true;
            player.npcTypeNoAggro[NPCID.Slimer] = true;
            player.npcTypeNoAggro[NPCID.GiantFlyingFox] = true;
            player.npcTypeNoAggro[NPCID.Vampire] = true;

            if (player.ZoneOverworldHeight && Main.dayTime)
            {
                int damages = 4;
                player.lifeRegen -= damages;

                Dust.NewDust(player.position, player.width, player.height, DustID.GoldFlame);
            }
        }
    }

    public class DraculaPendantDrop : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Vampire && Main.rand.NextBool(50))
                Item.NewItem(npc.getRect(), mod.ItemType<DraculaPendant>());
        }
    }
}
