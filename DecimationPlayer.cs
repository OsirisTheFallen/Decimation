using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation
{
    public class DecimationPlayer : ModPlayer
    {
        public bool closeToEnchantedAnvil = false;
        public bool isJestersQueverEquiped = false;
        public bool deadeyesQuiverEquipped = false;

        public override void ResetEffects()
        {
            closeToEnchantedAnvil = false;
            isJestersQueverEquiped = false;
            deadeyesQuiverEquipped = false;
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (isJestersQueverEquiped && type == ProjectileID.WoodenArrowFriendly)
                type = ProjectileID.JestersArrow;

            if (deadeyesQuiverEquipped && (type == ProjectileID.WoodenArrowFriendly || type == ProjectileID.Bullet))
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                    type = ProjectileID.IchorArrow;
                else
                    type = ProjectileID.ChlorophyteBullet;

                speedX *= 1.15f;
                speedY *= 1.15f;
            }

            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (deadeyesQuiverEquipped && Main.rand.Next(20) > 3)
                return false;

            return base.ConsumeAmmo(weapon, ammo);
        }
    }
}
