using Decimation.Buffs;
using Decimation.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    class ArachnusFireball : ModProjectile
    {
        public override string Texture
        {
            get { return "Terraria/Projectile_" + ProjectileID.Fireball; }
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Fireball);
            projectile.light = 1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType<Singed>(), 120);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Singed>(), 120);
        }
    }
}
