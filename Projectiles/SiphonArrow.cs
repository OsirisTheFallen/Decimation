using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    class SiphonArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            aiType = ProjectileID.WoodenArrowFriendly;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Heal(damage);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Heal(damage);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Heal(damage);
        }

        private void Heal(int damage)
        {
            int healAmount = (int)(damage * (Main.rand.Next(26) / 100f));

            Player player = Main.player[projectile.owner];
            player.statLife += healAmount;
            player.HealEffect(healAmount);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return true;
        }
    }
}
