using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons.Arachnus
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
            item.value = 450000;
            item.rare = 10;
            item.maxStack = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.melee = true;
            item.damage = 850;
            item.shoot = mod.ProjectileType("ArchingSolarBlade");
            item.shootSpeed = 15;
            item.crit = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.knockBack = 4;
        }
    }
}
