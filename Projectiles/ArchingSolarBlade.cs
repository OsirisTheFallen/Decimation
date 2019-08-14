using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    internal class ArchingSolarBlade : DecimationProjectile
    {
        //public override void SetStaticDefaults()
        //{
        //    DisplayName.SetDefault("Arching Solar Blade");
        //}

        protected override void Init()
        {
            width = 42;
            height = 46;
            tileCollide = false;
            penetrate = 30;
            ignoreWater = true;
            aiStyle = 3;
            aiType = ProjectileID.BloodyMachete;
            light = 1f;

            projectile.extraUpdates = 1;
        }
    }
}
