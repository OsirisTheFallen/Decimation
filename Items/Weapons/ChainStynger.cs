using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    class ChainStynger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chain Stynger");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Stynger);
            item.damage *= 2;
            item.crit *= 2;
            item.knockBack *= 2;
            item.useTime /= 2;
            item.useAnimation /= 2;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Singed"), 480);
        }
    }
}
