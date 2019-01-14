using Decimation.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons.Bloodshot
{
    public class Umbra : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Umbra");
            Tooltip.SetDefault("Turns wooden arrows into siphon arrows.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 20000;
            item.rare = 1;
            item.maxStack = 1;
            item.ranged = true;
            item.damage = 18;
            item.useAmmo = AmmoID.Arrow;
            item.noMelee = true;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 6.8f;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType<SiphonArrow>();
            return true;
        }
    }
}