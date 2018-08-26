using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    class GlaiveWeaver : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glaive Weaver");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 46;
            item.value = 1000000;
            item.rare = 8;
            item.maxStack = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.melee = true;
            item.damage = 450;
            item.shoot = mod.ProjectileType("ArchingSolarBlade");
            item.shootSpeed = 15;
            item.crit = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.knockBack = 4;
        }
    }
}
