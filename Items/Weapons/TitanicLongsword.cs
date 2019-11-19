using Decimation.Buffs.Debuffs;
using Decimation.Items.Ores;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class TitanicLongsword : DecimationWeapon
    {
        protected override string ItemName => "Titanic Longsword";
        protected override int Damages => 145;

        protected override void InitWeapon()
        {
            useTime = 21;
            useAnimation = 21;
            criticalStrikeChance = 14;
            knockBack = 7;
            this.item.value = Item.buyPrice(gold: 45);
            rarity = Rarity.LightPurple;
            width = 84;
            height = 84;
            autoReuse = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool criticalStrikeChance)
        {
            target.AddBuff(ModContent.BuffType<Amnesia>(), 480);
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, ModContent.TileType<TitanForge>());

            recipe.AddIngredient(ModContent.ItemType<TitaniteBar>(), 12);
            recipe.AddIngredient(ItemID.SoulofMight, 15);

            return recipe;
        }
    }
}