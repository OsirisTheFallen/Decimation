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
    class BloodBeam : ModProjectile
    {
        public override string Texture
        {
            get { return "Terraria/Projectile_" + ProjectileID.CursedFlameHostile; }
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 40;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.damage = 25;
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override void AI()
        {
            projectile.velocity.Y += (60 - projectile.timeLeft) * 0.005f;

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
