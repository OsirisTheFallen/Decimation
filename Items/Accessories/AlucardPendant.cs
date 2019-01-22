using Decimation.Buffs.Buffs;
using Decimation.Items.Misc.Souls;
using Decimation.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    class AlucardPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gives Vampire buff\n"
                + "Bats will be friendly"
                );
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 62;
            item.accessory = true;
            item.maxStack = 1;
            item.rare = 6;
            item.value = 40000;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<DraculaPendant>());
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(mod.ItemType<SoulofTime>(), 10);
            recipe.AddIngredient(ItemID.HolyWater, 5);
            recipe.AddTile(mod.TileType<EnchantedAnvil>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
