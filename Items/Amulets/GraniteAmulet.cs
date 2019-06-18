using Decimation.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    class GraniteAmulet : Amulet
    {
        public override AmuletClasses AmuletClass
        {
            get { return AmuletClasses.TANK; }
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 30;
        }

        public override void UpdateAmulet(Player player)
        {
            player.statDefense += 1;

            if (player.statLife >= player.statLifeMax * 0.75f)
            {
                player.moveSpeed *= 0.95f;
                player.statDefense = (int)(player.statDefense * 1.05f);
                player.noKnockback = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<GraniteLinedTunic>());
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Granite, 6);
            recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override AmuletTooltip GetAmuletTooltips()
        {
            return new AmuletTooltip(this)
                .addEffect("+1 to defense")
                .addEffect("-3% of incoming damage to team members (WIP)")
                .addEffect("When above " + (Main.LocalPlayer.statLifeMax * 0.75f) + " hp, your are slowed by 5%, but your defense is increased by 5% and the knockback is cancelled")
                .addSynergy("Cobalt Shield, Ankh Shield, Paladins Shield and Obsidian Shield gives 10% chance to block attacks");
        }
    }
}
