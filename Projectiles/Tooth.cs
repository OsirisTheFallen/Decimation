using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    class Tooth : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 18;
            projectile.aiStyle = 1;
            projectile.damage = 20;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.friendly = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int regen = (int)(damage * 0.08f);
            Player owner = Main.player[projectile.owner];

            owner.statLife += regen;
            owner.HealEffect(regen);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            int regen = (int)(damage * 0.08f);
            Player owner = Main.player[projectile.owner];

            owner.statLife += regen;
            owner.HealEffect(regen);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            int regen = (int)(damage * 0.08f);
            Player owner = Main.player[projectile.owner];

            owner.statLife += regen;
            owner.HealEffect(regen);
        }
    }
}
