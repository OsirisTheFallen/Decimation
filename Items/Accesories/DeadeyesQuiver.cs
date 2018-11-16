using Terraria.ModLoader;
using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace testmod.Items
{
    public class DeadeyesQuiver : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into ichor arrows\n+16% ranged damage\n+15% arrow and bullet velocity\n15% chance not to consume ammo\nTurns musket balls into chlorophyte bullets\n+2% ranged crit chance");
            DisplayName.SetDefault("Deadeye's Quiver");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            item.rare = 10;
            item.value = 150000;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(null, "JestersQuiver");
            r.AddIngredient(null, "SoulofKight");
            r.AddIngredient(ItemID.SoulofSight, 15);
            r.AddIngredient(ItemID.SoulofFright, 15);
            r.AddIngredient(null, "EndlessPouchOfLife");
            r.AddIngredient(null, "RedThread", 5);
            r.AddIngredient(ItemID.FlaskofIchor, 5);
            r.AddIngredient(ItemID.BlackDye, 3);
            r.AddIngredient(ItemID.RedDye, 3);
            r.SetResult(this);
            r.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.16f;
            player.rangedCrit += 2;
            player.GetModPlayer<DecimationPlayer>().DeadeyesQuiverEquipped = true;
        }
    }



    public class DecimationPlayer : ModPlayer
    {
        public bool DeadeyesQuiverEquipped = false;
        public override void ResetEffects()
        {
            DeadeyesQuiverEquipped = false;
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (DeadeyesQuiverEquipped && (type == ProjectileID.WoodenArrowFriendly || type == ProjectileID.Bullet))
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
            if (DeadeyesQuiverEquipped && Main.rand.Next(20) > 3)
                return false;

            return base.ConsumeAmmo(weapon, ammo);
        }
    }
}
