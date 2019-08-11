using Decimation.Buffs.Buffs;
using Decimation.Items.Misc.Souls;
using Decimation.Tiles;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class AlucardPendant : DecimationAccessory
    {
        protected override string ItemName => "Alucard Pendant";

        protected override string ItemTooltip => "Stronger than your average vampire\n" +
                                                 "Gives Vampire buff\n"
                                                 + "Bats will be friendly";

        protected override void InitAccessory()
        {
            width = 46;
            height = 62;
            rarity = Rarity.LightPurple;
            item.value = Item.buyPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(mod.BuffType<Vampire>(), 1);

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
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { mod.TileType<EnchantedAnvil>() }, false);

            recipe.AddIngredient(mod.ItemType<DraculaPendant>());
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(mod.ItemType<SoulofTime>(), 10);
            recipe.AddIngredient(ItemID.HolyWater, 5);

            return new List<ModRecipe>() {recipe};
        }
    }
}
