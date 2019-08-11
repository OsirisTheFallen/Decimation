using Decimation.Utils;
using Terraria;

namespace Decimation.Items.Weapons
{
    internal abstract class DecimationWeapon : DecimationItem
    {
        protected int criticalStrikeChance = 0; // Percents
        protected float knockBack = 0;    // https://terraria.gamepedia.com/Knockback
        protected float shootSpeed = 0;

        protected virtual DamageType DamagesType { get; } = DamageType.MELEE;
        protected virtual string Projectile { get; } = "DefaultProjectile";
        protected virtual bool VanillaProjectile { get; } = false;

        protected abstract int Damages { get; }
        protected abstract void InitWeapon();

        protected override void Init()
        {
            useStyle = 1;

            autoReuse = false;
            item.useTurn = true;
            item.maxStack = 1;

            switch (DamagesType)
            {
                case DamageType.MAGIC:
                    item.magic = true;
                    break;
                case DamageType.RANGED:
                    item.ranged = true;
                    break;
                case DamageType.THROW:
                    item.thrown = true;
                    break;
                default:
                    item.melee = true;
                    break;
            }

            InitWeapon();

            item.damage = Damages;
            item.crit = criticalStrikeChance;
            item.knockBack = knockBack;
            item.shoot = ItemUtils.GetIdFromName(Projectile, typeof(Projectile), VanillaProjectile);
            item.shootSpeed = shootSpeed;

            if (!item.melee)
            {
                item.noMelee = true;
            }
        }

        protected enum DamageType
        {
            MELEE,
            MAGIC,
            THROW,
            RANGED
        }
    }
}
