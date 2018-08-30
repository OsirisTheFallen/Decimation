using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    class ArchingSolarBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arching Solar Blade");
        }

        public override void SetDefaults()
        {
            projectile.width = 42;       //projectile width
            projectile.height = 46;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.melee = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 30;      //how many npc will penetrate
            projectile.timeLeft = 180;   //how many time this projectile has before disepire
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.aiStyle = 3;
            aiType = ProjectileID.BloodyMachete;
            projectile.light = 1f;
        }
    }
}
