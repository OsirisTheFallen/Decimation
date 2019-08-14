using System;
using Decimation.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class Tooth : DecimationProjectile
    {
        protected override void Init()
        {
            width = 10;
            height = 18;
            aiStyle = 1;
            damages = 20;
            tileCollide = true;
            ignoreWater = false;
            penetrate = 1;
            timeLeft = 600;

            damageType = DecimationWeapon.DamageType.RANGED;
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
