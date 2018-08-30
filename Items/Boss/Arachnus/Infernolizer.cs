using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Arachnus
{
    class Infernolizer : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.HeatRay);
            item.damage *= 16;
            item.crit *= 2;
            item.knockBack *= 2;
            item.useTime /= 2;
            item.useAnimation /= 2;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(new Vector2(position.X, position.Y - 8), new Vector2(speedX, speedY), type, damage, knockBack);
            return true;
        }
    }
}
