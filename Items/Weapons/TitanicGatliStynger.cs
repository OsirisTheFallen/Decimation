using System;
using Decimation.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class TitanicGatliStynger : DecimationWeapon
    {
        protected override string ItemName => "Titanic Gatli Stynger";
        protected override string ItemTooltip => "Feel the rage of Kronos by your side.";
        protected override bool IsClone => true;
        protected override DamageType DamagesType => DamageType.RANGED;
        protected override int Damages => 950;
        protected override string Projectile => "TitanicStyngerBolt";

        protected override void InitWeapon()
        {
            item.CloneDefaults(ItemID.Stynger);

            width = 52;
            height = 26;
            criticalStrikeChance = 8;
            knockBack = 11;
            useTime = 10;
            useAnimation = 10;
            rarity = Rarity.Red;
            autoReuse = true;
            shootSpeed = 9f;
            value = Item.buyPrice(0, 60);
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, mod.TileType<TitanForge>());

            recipe.AddIngredient(null, "ChainStynger");
            recipe.AddIngredient(null, "TitaniteBar", 15);
            // recipe.AddIngredient(null, "CondensedMight", 5);
            recipe.AddIngredient(null, "DenziumBar");

            return recipe;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
         {
            type = mod.ProjectileType<Projectiles.TitanicStyngerBolt>();
            return true;
        }
    }
}
