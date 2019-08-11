﻿using System;
using Decimation.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class TitanicRepeater : DecimationWeapon
    {
        protected override string ItemName => "Titanic Repeater";
        protected override DamageType DamagesType => DamageType.RANGED;
        protected override int Damages => 120;

        protected override void InitWeapon()
        {
            width = 56;
            height = 36;
            criticalStrikeChance = 20;
            useStyle = 5;
            useTime = 12;
            useAnimation = 12;
            knockBack = 7;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            useSound = SoundID.Item5;
            shootSpeed = 25;
            autoReuse = true;
            item.value = Item.buyPrice(gold: 45);
            rarity = Rarity.LightPurple;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, mod.TileType<TitanForge>());

            recipe.AddIngredient(mod.ItemType("TitaniteBar"), 12);
            recipe.AddIngredient(ItemID.SoulofMight, 15);

            return recipe;
        }
    }
}
