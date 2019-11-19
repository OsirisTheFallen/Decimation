using Decimation.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Decimation.Tiles.ShrineoftheMoltenOne
{
    class RedHotSpike : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            TileID.Sets.NotReallySolid[Type] = true;
            dustType = DustID.Stone;
            drop = ModContent.ItemType<Items.Placeable.ShrineoftheMoltenOne.RedHotSpike>();
            AddMapEntry(new Color(196, 35, 0));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool Dangersense(int i, int j, Player player)
        {
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;

            float playerX = player.position.X;
            float playerY = player.position.Y;

            if (playerX / 16 - i <= 2 && playerX / 16 - i >= -2 && playerY / 16 - j <= 2 && playerY / 16 - j >= -4.3f)
                player.AddBuff(ModContent.BuffType<Singed>(), 300);
            if (playerX / 16 - i <= 1 && playerX / 16 - i >= -1.25f && playerY / 16 - j <= 1.1f && playerY / 16 - j >= -3.3f)
                player.Hurt(PlayerDeathReason.LegacyDefault(), 100, 0);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return DecimationWorld.downedArachnus;
        }
    }
}
