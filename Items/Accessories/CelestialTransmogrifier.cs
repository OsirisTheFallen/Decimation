using Decimation.Buffs.Buffs;
using Decimation.Items.Misc.Souls;
using Decimation.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    class CelestialTransmogrifier : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Change form on a whim\n\n"
                + "Gives Werewolf buff\n"
                + "Transforms holder into merfolk when entering water\n"
                + "Gives Celestial Stone effects\n"
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
            player.AddBuff(mod.BuffType<Werepire>(), 1);

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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<AlucardPendant>());
            recipe.AddIngredient(ItemID.CelestialShell);
            recipe.AddIngredient(mod.ItemType<SoulofSpite>(), 20);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(mod.TileType<ChlorophyteAnvil>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
