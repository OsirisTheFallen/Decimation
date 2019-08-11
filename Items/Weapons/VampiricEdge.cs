using Decimation.Items.Misc.Souls;
using Decimation.Items.Weapons.Bloodshot;
using Decimation.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class VampiricEdge : DecimationWeapon
    {
        private int shootDelay = 72;
        private int timeToShoot = 72;

        protected override string ItemName => "Vampiric Edge";
        protected override int Damages => 54;
        protected override string Projectile => "Tooth";

        protected override void InitWeapon()
        {
            width = 46;
            height = 52;
            criticalStrikeChance = 6;
            knockBack = 4.5f;
            useTime = 20;
            useAnimation = 20;
            shootSpeed = 5f;
            item.value = Item.buyPrice(0, 3, 0, 0);
            rarity = Rarity.Green;
            autoReuse = true;
        }

        public override void UpdateInventory(Player player)
        {
            if (timeToShoot > 0) timeToShoot--;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (timeToShoot > 0) return false;

            timeToShoot = shootDelay;

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.MythrilAnvil, true);

            recipe.AddIngredient(ItemID.BloodButcherer);
            recipe.AddIngredient(mod.ItemType<VampiricShiv>());
            recipe.AddIngredient(mod.ItemType<SoulofTime>(), 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);

            return recipe;
        }
    }
}
