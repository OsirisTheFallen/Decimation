using System;
using Decimation.Items.Ores;
using Decimation.Items.Weapons.Arachnus;
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

            knockBack = 11;
            shootSpeed = 9f;
            criticalStrikeChance = 8;

            item.width = 52;
            item.height = 26;
            item.useTime = 10;
            item.useAnimation = 10;
            item.rare = 10;
            item.autoReuse = true;
            item.value = Item.buyPrice(0, 60);
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, mod.TileType<TitanForge>());

            recipe.AddIngredient(mod.ItemType<ChainStynger>());
            recipe.AddIngredient(mod.ItemType<TitaniteBar>(), 15);
            // TODO recipe.AddIngredient(null, "CondensedMight", 5);
            recipe.AddIngredient(mod.ItemType<DenziumBar>());

            return recipe;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
         {
            type = mod.ProjectileType<Projectiles.TitanicStyngerBolt>();
            return true;
        }
    }
}
