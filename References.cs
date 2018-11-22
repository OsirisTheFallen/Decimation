using System;
using System.Collections;
using Terraria.ID;
using Decimation.Projectiles;

namespace Decimation
{
    class References
    {
        public static ArrayList bullets = new ArrayList()
        {
            ProjectileID.Bullet,
            ProjectileID.ChlorophyteBullet,
            ProjectileID.CrystalBullet,
            ProjectileID.ExplosiveBullet,
            ProjectileID.GoldenBullet,
            ProjectileID.IchorBullet,
            ProjectileID.MoonlordBullet,
            ProjectileID.CursedBullet,
            ProjectileID.MeteorShot,
            ProjectileID.NanoBullet,
            ProjectileID.PartyBullet,
            ProjectileID.BulletHighVelocity,
            ProjectileID.RainbowRodBullet,
            ProjectileID.SniperBullet,
            ProjectileID.VenomBullet,
            ProjectileID.BulletDeadeye,
            ProjectileID.BulletSnowman
        };

        public static ArrayList styngerBolts = new ArrayList()
        {
            ProjectileID.Stynger,
            Decimation.decimation.ProjectileType<MoltenStyngerBolt>(),
            Decimation.decimation.ProjectileType<TitanicStyngerBolt>()
        };
    }
}
