using Decimation.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
    class ScarabEndurance : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Scarab Endurance");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();

            if (modPlayer.scarabEnduranceBuffTimeCounter >= 180)
            {
                if (modPlayer.scarabCounter < 3)
                {
                    modPlayer.scarabCounter++;
                    modPlayer.scarabs[modPlayer.scarabCounter - 1] = Projectile.NewProjectile(player.Center, new Vector2(0, 0), mod.ProjectileType<Scarab>(), 0, 0, 255, player.whoAmI);
                }
                modPlayer.scarabEnduranceBuffTimeCounter = 0;
            }
            modPlayer.scarabEnduranceBuffTimeCounter++;

            for (int i = 0; i < modPlayer.scarabCounter; i++)
                player.statDefense += (int)(modPlayer.oldStatDefense * 0.15f);
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            DecimationPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DecimationPlayer>();
            int defenseAdded = 0;

            for (int i = 0; i < modPlayer.scarabCounter; i++)
                defenseAdded += (int)(modPlayer.oldStatDefense * 0.15f);

            tip += "Summons scarabs to protect you.";
            tip += "\nGive 15% more defense for each scarabs alive.";
            tip += "\n3 scarabs maximum";
            tip += "\nCurrently, you have " + defenseAdded + " defense added.";
        }
    }

    public class ScarabEnduranceEffect : GlobalNPC
    {
        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (target.HasBuff(mod.BuffType<ScarabEndurance>())) npc.AddBuff(BuffID.OnFire, 300);
        }
    }
}
