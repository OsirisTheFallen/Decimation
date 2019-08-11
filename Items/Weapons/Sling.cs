using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace Decimation.Items.Weapons
{
    internal class Sling : DecimationWeapon
    {
			protected override string ItemName => "Sling";
			protected override string ItemTooltip => "Uses pebbles and marbles as ammo";
            protected override DamageType DamagesType => DamageType.RANGED;
            protected override int Damages => 9;

        protected override void InitWeapon()
        {
            width = 30; 
            height = 22; 
            useTime = 16; 
            useAnimation = 16;  
            item.shoot = 1; 
			item.useAmmo = mod.ItemType("Pebble");
            knockBack = 6; 
            rarity = Rarity.Orange; 
            useSound = SoundID.Item5;
            shootSpeed = 10f;
            criticalStrikeChance = 10; 
		}	
	}
}