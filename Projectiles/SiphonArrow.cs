using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Decimation.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class SiphonArrow : DecimationProjectile
    {
        protected override void Init()
        {
            width = 14;
            height = 32;
            aiStyle = 1;
            damageType = DecimationWeapon.DamageType.RANGED;
            penetrate = 1;
            timeLeft = 600;
            tileCollide = true;
            projectile.arrow = true;
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
