using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class BlastofHeat : DecimationProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blast of Heat");
        }

        protected override void Init()
        {
            width = 6;
            height = 6;
            aiStyle = 23;
            hostile = true;
            projectile.alpha = 255;
            penetrate = -1;
            projectile.extraUpdates = 3;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
                target.AddBuff(BuffID.OnFire, 600);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(2) == 0)
                target.AddBuff(BuffID.OnFire, 600);
            base.OnHitPlayer(target, damage, crit);
        }
    }
}
