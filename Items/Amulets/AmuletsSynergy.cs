using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace Decimation.Items.Amulets
{
    class AmuletsSynergy
    {
        private Decimation decimation;

        public AmuletsSynergy(Decimation decimation)
        {
            this.decimation = decimation;
        }

        public void OnHitPlayer(Item amulet, DecimationPlayer modPlayer, ref int damages)
        {
            if (amulet.type == decimation.ItemType<GraniteAmulet>())
            {
                if (modPlayer.hasShield && Main.rand.NextBool(10))
                {
                    damages = 0;
                    CombatText.NewText(modPlayer.player.getRect(), Color.MediumPurple, "Blocked");
                }
            }
        }
    }

    public class GraniteAmuletDetectShields : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ItemID.CobaltShield || item.type == ItemID.AnkhShield || item.type == ItemID.PaladinsShield || item.type == ItemID.ObsidianShield) player.GetModPlayer<DecimationPlayer>().hasShield = true;
        }
    }
}
