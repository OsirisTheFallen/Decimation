using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Buffs.Debuffs;
using Microsoft.Xna.Framework;

namespace Decimation.Projectiles
{
    class Ember : ModProjectile
    {
        public override string Texture
        {
            get
            {
                return "Terraria/Projectile_" + ProjectileID.Flames;
            }
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.light = 0.8f;
            projectile.damage = 25;
            projectile.timeLeft = 60;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0, 0, 0, new Microsoft.Xna.Framework.Color(240, 94, 27));
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
