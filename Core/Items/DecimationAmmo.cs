using Decimation.Core.Util;
using Terraria;

namespace Decimation.Core.Items
{
    public abstract class DecimationAmmo : DecimationItem
    {
        protected int damages = 0;
        protected float projKnockBack = 0;

        protected abstract string Projectile { get; }
        protected abstract int Ammo { get; }
        protected virtual bool VanillaProjectile { get; } = false;
        protected abstract void InitAmmo();

        protected sealed override void Init()
        {
            this.item.maxStack = 999;
            this.item.consumable = true;

            InitAmmo();

            this.item.damage = damages;
            this.item.knockBack = projKnockBack;

            this.item.shoot = ItemUtils.GetIdFromName(this.Projectile, typeof(Projectile), this.VanillaProjectile);
            this.item.ammo = this.Ammo;

            this.item.ranged = true;
        }
    }
}