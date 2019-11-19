using Decimation.Items.Ores;
using Decimation.Items.Weapons.Arachnus;
using Decimation.Projectiles;
using Decimation.Tiles;
using Decimation.Core.Items;
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
            this.item.CloneDefaults(ItemID.Stynger);

            knockBack = 11;
            shootSpeed = 9f;
            criticalStrikeChance = 8;

            this.item.width = 52;
            this.item.height = 26;
            this.item.useTime = 10;
            this.item.useAnimation = 10;
            this.item.rare = 10;
            this.item.autoReuse = true;
            this.item.value = Item.buyPrice(0, 60);
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, ModContent.TileType<TitanForge>());

            recipe.AddIngredient(ModContent.ItemType<ChainStynger>());
            recipe.AddIngredient(ModContent.ItemType<TitaniteBar>(), 15);
            // TODO recipe.AddIngredient(null, "CondensedMight", 5);
            recipe.AddIngredient(ModContent.ItemType<DenziumBar>());

            return recipe;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            type = ModContent.ProjectileType<TitanicStyngerBolt>();
            return true;
        }
    }
}