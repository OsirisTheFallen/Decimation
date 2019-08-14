using Decimation.Buffs.Debuffs;
using Decimation.Dusts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class BloodBeam : DecimationProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.CursedFlameHostile;

        protected override void Init()
        {
            width = 26;
            height = 26;
            aiStyle = -1;
            penetrate = -1;
            projectile.alpha = 255;
            timeLeft = 40;
            tileCollide = false;
            ignoreWater = true;
            damages = 25;
            hostile = true;
        }

        public override void AI()
        {
            projectile.velocity.Y += (60 - timeLeft) * 0.005f;

            Dust.NewDust(projectile.position, 26, 26, mod.DustType<Blood>());
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
                target.AddBuff(mod.BuffType<Slimed>(), 600);

            int damages = Main.rand.Next(5, 11);
            target.Hurt(PlayerDeathReason.LegacyDefault(), damages, 0);

            NPC bloodshotEye = Main.npc[(int)projectile.ai[0]];
            bloodshotEye.life += damages;
            bloodshotEye.HealEffect(damages);
        }
    }
}
