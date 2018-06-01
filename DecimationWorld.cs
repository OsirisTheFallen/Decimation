using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using System.Linq;
 
 
namespace Decimation
{
    public class DecimationWorld : ModWorld
    {
		public static bool downedBloodshotEye = false;		
		public override void Initialize()
		{
			downedBloodshotEye = false;
			downedDuneWorm = false;
		}
		public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedBloodshotEye) downed.Add("downedBloodshotEye");
			if (downedDuneWorm) downed.Add("downedDuneWorm");
            return new TagCompound	{
                {"downed", downed},
			};				
        }
		public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedBloodshotEye = downed.Contains("downedBloodshotEye");
			downedDuneWorm = downed.Contains("downedDuneWorm");
        }
		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = downedBloodshotEye; //+1 flag number for each new boss
			flags[1] = downedDuneWorm;
		}
		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedBloodshotEye = flags[0];
			downedDuneWorm = flags[1];
		}
		public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if(loadVersion == 1)
            {         
                byte flags=reader.ReadByte();
                DecimationWorld.downedBloodshotEye=((flags&1)!=0);
				DecimationWorld.downedDuneWorm=((flags&2)!=0); //double flag numbers with each new boss
            }
            else if(loadVersion == 2)
            {  
                byte flags=reader.ReadByte();
				byte flags2=reader.ReadByte();		
                DecimationWorld.downedBloodshotEye=((flags&1)!=0);
				DecimationWorld.downedDuneWorm=((flags&2)!=0);
            }		
	}
}	