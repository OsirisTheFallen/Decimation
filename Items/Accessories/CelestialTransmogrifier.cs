﻿using System.Collections.Generic;
using Decimation.Buffs.Buffs;
using Decimation.Items.Misc.Souls;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class CelestialTransmogrifier : DecimationAccessory
    {
        protected override string ItemName => "Celestial Transmogrifier";

        protected override string ItemTooltip => "Change form on a whim\n\n"
                                                 + "Gives Werewolf buff\n"
                                                 + "Transforms holder into merfolk when entering water\n"
                                                 + "Gives Celestial Stone effects\n"
                                                 + "Bats will be friendly";

        protected override void InitAccessory()
        {
            width = 46;
            height = 62;
            rarity = Rarity.LightPurple;

            this.item.value = Item.buyPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(this.mod.BuffType<Werepire>(), 1);

            player.meleeSpeed *= 1.1f;
            player.meleeDamage *= 1.1f;
            player.magicDamage *= 1.1f;
            player.rangedDamage *= 1.1f;
            player.thrownDamage *= 1.1f;
            player.meleeCrit += 2;
            player.magicCrit += 2;
            player.rangedCrit += 2;
            player.thrownCrit += 2;
            player.lifeRegen += 1;
            player.statDefense += 4;
            player.tileSpeed *= 1.15f;
            player.minionKB *= 1.5f;

            player.npcTypeNoAggro[NPCID.CaveBat] = true;
            player.npcTypeNoAggro[NPCID.JungleBat] = true;
            player.npcTypeNoAggro[NPCID.Hellbat] = true;
            player.npcTypeNoAggro[NPCID.IceBat] = true;
            player.npcTypeNoAggro[NPCID.GiantBat] = true;
            player.npcTypeNoAggro[NPCID.IlluminantBat] = true;
            player.npcTypeNoAggro[NPCID.Lavabat] = true;
            player.npcTypeNoAggro[NPCID.Slimer] = true;
            player.npcTypeNoAggro[NPCID.GiantFlyingFox] = true;
            player.npcTypeNoAggro[NPCID.Vampire] = true;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {this.mod.TileType<ChlorophyteAnvil>()});

            recipe.AddIngredient(this.mod.ItemType<AlucardPendant>());
            recipe.AddIngredient(ItemID.CelestialShell);
            recipe.AddIngredient(this.mod.ItemType<SoulofSpite>(), 20);
            recipe.AddIngredient(ItemID.FallenStar, 5);

            return new List<ModRecipe> {recipe};
        }
    }
}