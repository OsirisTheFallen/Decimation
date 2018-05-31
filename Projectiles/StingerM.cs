using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{

    public class StingerM : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stinger");     //The English name of the projectile
		}			
        public override void SetDefaults()
        {
            projectile.width = 10;       //projectile width
            projectile.height = 18;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.melee = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 2;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time this projectile has before disepire
            projectile.extraUpdates = 1;
            projectile.ignoreWater = false;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (Main.rand.Next(2) == 0)
			{	
				target.AddBuff(BuffID.Poisoned, 100);
			}
        }
        
    }
}