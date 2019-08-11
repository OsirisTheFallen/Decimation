using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    internal class CreatorAmulet : Amulet
    {
        protected override string ItemName => "Creator's Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Creator;

        protected override void InitAmulet()
        {
            height = 32;
        }

        protected override void UpdateAmulet(Player player)
        {
            player.blockRange += 2;
            player.tileSpeed *= 1.04f;
            player.meleeSpeed *= 1.04f;
            player.pickSpeed *= 1.03f;

            player.AddBuff(BuffID.NightOwl, 1);
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1, 1, 1);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> { TileID.TinkerersWorkbench }, false);

            recipe.AddIngredient(mod.ItemType<MinersAmulet>());
            recipe.AddIngredient(mod.ItemType<BuildersAmulet>());
            recipe.AddIngredient(ItemID.MiningHelmet);

            return new List<ModRecipe> { recipe };
        }

        protected override void SetAmuletTooltips(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+4% tile placement speed")
                .AddEffect("+4% melee speed")
                .AddEffect("+4% mining speed")
                .AddEffect("+2 block interaction range")
                .AddEffect("Provides light")
                .AddEffect("Provides Night Owl buff");
        }
    }
}
