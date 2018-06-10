using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Tiles
{
	public class GildedSickle : GlobalTile
	{
		public override bool Drop(int i, int j, int type)
		{
			if(Main.LocalPlayer.HeldItem.type == mod.ItemType("GildedSickle")){
				if(type == TileID.Plants)
				{
					Main.LocalPlayer.AddBuff(BuffID.Swiftness, 60);
					Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Hay, Main.rand.Next(3, 11));
				}
			}
			return true;
		}
	}
}