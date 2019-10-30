using Decimation.Projectiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Decimation.Items.Weapons.Bloodshot
{
    internal class Umbra : DecimationWeapon
    {
        protected override string ItemName => "Umbra";
        protected override string ItemTooltip => "Turns wooden arrows into siphon arrows.";
        protected override DamageType DamagesType => DamageType.RANGED;
        protected override int Damages => 18;
        protected override string Projectile => "WoodenArrowFriendly";
        protected override bool VanillaProjectile => true;

        protected override void InitWeapon()
        {
            width = 20;
            height = 20;
            value = Item.buyPrice(0, 2);
            rarity = Rarity.Green;
            this.item.useAmmo = AmmoID.Arrow;
            shootSpeed = 6.8f;
            useTime = 26;
            useAnimation = 26;
            useStyle = 5;
            useSound = SoundID.Item5;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            type = this.mod.ProjectileType<SiphonArrow>();
            return true;
        }
    }
}