using Decimation.Core.Util;
using Terraria;

namespace Decimation.Core.Items
{
    public abstract class DecimationWeapon : DecimationItem
    {
        protected int criticalStrikeChance = 0; // Percents
        protected float knockBack = 0; // https://terraria.gamepedia.com/Knockback
        protected float shootSpeed = 0;

        protected virtual DamageType DamagesType { get; } = DamageType.MELEE;
        protected virtual string Projectile { get; }
        protected virtual string Ammo { get; }
        protected virtual bool VanillaProjectile { get; } = false;
        protected virtual bool VanillaAmmo { get; } = false;

        protected abstract int Damages { get; }
        protected abstract void InitWeapon();

        protected override void Init()
        {
            useStyle = 1;

            autoReuse = false;
            this.item.useTurn = true;
            this.item.maxStack = 1;

            switch (this.DamagesType)
            {
                case DamageType.MAGIC:
                    this.item.magic = true;
                    break;
                case DamageType.RANGED:
                    this.item.ranged = true;
                    break;
                case DamageType.THROW:
                    this.item.thrown = true;
                    break;
                default:
                    this.item.melee = true;
                    break;
            }

            InitWeapon();

            this.item.damage = this.Damages;
            this.item.crit = criticalStrikeChance;
            this.item.knockBack = knockBack;
            this.item.shootSpeed = shootSpeed;

            if (this.Projectile != null)
                this.item.shoot = ItemUtils.GetIdFromName(this.Projectile, typeof(Projectile), this.VanillaProjectile);

            if (this.Ammo != null)
                this.item.useAmmo = ItemUtils.GetIdFromName(this.Ammo, typeof(Item), this.VanillaAmmo);

            if (!this.item.melee) this.item.noMelee = true;
        }

        public enum DamageType
        {
            MELEE,
            MAGIC,
            THROW,
            RANGED
        }
    }
}