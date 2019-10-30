using System.Collections;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Core.Util
{
    public class References
    {
        public static Mod mod;

        public static ArrayList bullets = new ArrayList
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

        // TODO
        //public static ArrayList styngerBolts = new ArrayList()
        //{
        //    ProjectileID.Stynger,
        //    Decimation.Instance.ProjectileType<MoltenStyngerBolt>(),
        //    Decimation.Instance.ProjectileType<TitanicStyngerBolt>()
        //};
    }

    public enum Rarity
    {
        Gray = -1,
        White = 0,
        Blue = 1,
        Green = 2,
        Orange = 3,
        LightRed = 4,
        Pink = 5,
        LightPurple = 6,
        Lime = 7,
        Yellow = 8,
        Cyan = 9,
        Red = 10,
        Purple = 11,
        Rainbow = -12,
        Quest = -11
    }
}