using System.Collections.Generic;
using Decimation.Items.Ores;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ammo
{
    internal class TitanicStyngerBolt : DecimationAmmo
    {
        protected override string ItemName => "Titanic Stynger Bolt";
        protected override string ItemTooltip => "Explodes into deadly shrapnel.";
        protected override string Projectile => "TitanicStyngerBolt";
        protected override int Ammo => AmmoID.StyngerBolt;

        protected override void InitAmmo()
        {
            damages = 35;
            projKnockBack = 2;
            rarity = Rarity.Orange;
            width = 8;
            height = 8;
            value = Item.buyPrice(0, 0, 10);
            consumable = true;

            this.item.shootSpeed = 2f;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.NextBool(100))
                target.AddBuff(this.mod.BuffType("Amnesia"), 600);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(100))
                target.AddBuff(this.mod.BuffType("Amnesia"), 600);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 50, new List<int> {this.mod.TileType<TitanForge>()});

            recipe.AddIngredient(ItemID.StyngerBolt, 50);
            recipe.AddIngredient(this.mod.ItemType<TitaniteBar>(), 3);

            return new List<ModRecipe> {recipe};
        }
    }
}