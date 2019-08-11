using Decimation.Utils;
using Terraria;

namespace Decimation.Items.Ammo
{
    internal abstract class DecimationAmmo : DecimationItem
    {
        protected int damages = 0;
        protected int projKnockBack = 0;

        protected abstract string Projectile { get; }
        protected abstract int Ammo { get; }
        protected virtual bool ShootVanilla { get; } = false;
        protected abstract void InitAmmo();

        protected sealed override void Init()
        {
            item.maxStack = 999;
            item.consumable = true;

            InitAmmo();

            item.damage = damages;
            item.knockBack = projKnockBack;

            item.shoot = ItemUtils.GetIdFromName(Projectile, typeof(Projectile), ShootVanilla);
            item.ammo = Ammo;

            item.ranged = true;
        }
    }
}
