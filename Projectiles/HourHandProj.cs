using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Decimation.Projectiles
{
    public class HourHandProj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hour Hand Projectile");
			Main.projFrames[projectile.type] = 4;
			//Main.aiType = 8;
		}

        public override void SetDefaults()
        {
			projectile.arrow = false;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.aiStyle = 8;
			
			//projectile.CloneDefaults(ProjectileID.WaterBolt);
        }

 		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0f, 0f, 1f);
			projectile.rotation = projectile.velocity.ToRotation();
		} 


    }
}