using Decimation.Core.Items;
using Decimation.Core.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Decimation.Items.Weapons.Arachnus
{
    internal class Infernolizer : DecimationWeapon
    {
        protected override string ItemName => "Infernolizer";
        protected override string ItemTooltip => "Releases flares upon your foes";
        protected override bool IsClone => true;
        protected override int Damages => 880;
        protected override DamageType DamagesType => DamageType.MAGIC;
        protected override bool VanillaProjectile => true;
        protected override string Projectile => "HeatRay";

        protected override void InitWeapon()
        {
            this.item.CloneDefaults(ItemID.HeatRay);

            criticalStrikeChance *= 2;
            knockBack *= 2;
            useTime /= 2;
            useAnimation /= 2;
            value = Item.buyPrice(0, 45);
            rarity = Rarity.Red;
            shootSpeed = 15f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            Terraria.Projectile.NewProjectile(new Vector2(position.X, position.Y - 8), new Vector2(speedX, speedY),
                type, damage, knockBack);
            return true;
        }
    }
}