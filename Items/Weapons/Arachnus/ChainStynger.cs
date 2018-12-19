using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons.Arachnus
{
    class ChainStynger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chain Stynger");
            Tooltip.SetDefault("Unleash the fury");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Stynger);
            item.damage = 800;
            item.crit *= 2;
            item.knockBack *= 2;
            item.useTime /= 2;
            item.useAnimation /= 2;
            item.rare = 10;
            item.value = 450000;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Singed"), 480);
        }
    }
}
