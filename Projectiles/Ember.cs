using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Buffs.Debuffs;
using Microsoft.Xna.Framework;

namespace Decimation.Projectiles
{
   internal class Ember : DecimationProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Flames;

        protected override void Init()
        {
            width = 20;
            height = 20;
            aiStyle = -1;
            projectile.alpha = 255;
            penetrate = 1;
            light = 0.8f;
            damages = 25;
            timeLeft = 60;
            tileCollide = false;
            ignoreWater = true;
        }

        public override void AI()
        {
            Dust.NewDust(projectile.position, width, height, 6, 0, 0, 0, new Microsoft.Xna.Framework.Color(240, 94, 27));
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType<Slimed>(), 300);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Slimed>(), 300);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Slimed>(), 300);
        }
    }
}
