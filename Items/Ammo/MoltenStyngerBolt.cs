using System.Collections.Generic;
using Decimation.Buffs.Debuffs;
using Decimation.Items.Misc;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ammo
{
    internal class MoltenStyngerBolt : DecimationAmmo
    {
        protected override string ItemName => "Molten Stynger Bolt";
        protected override string ItemTooltip => "Explodes into molten shrapnel.";
        protected override string Projectile => "MoltenStyngerBolt";
        protected override int Ammo => AmmoID.StyngerBolt;

        protected override void InitAmmo()
        {
            width = 8;
            height = 8;
            damages = 25;
            projKnockBack = 1;
            rarity = Rarity.Orange;
            value = Item.buyPrice(0, 0, 10);
            consumable = true;

            this.item.shootSpeed = 2f;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool isCritical)
        {
            target.AddBuff(ModContent.BuffType<Singed>(), 600);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool isCritical)
        {
            player.AddBuff(ModContent.BuffType<Singed>(), 600);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 50, new List<int> {ModContent.TileType<TitanForge>()});

            recipe.AddIngredient(ItemID.StyngerBolt, 50);
            recipe.AddIngredient(ModContent.ItemType<Thermoplasm>(), 5);

            return new List<ModRecipe> {recipe};
        }
    }
}