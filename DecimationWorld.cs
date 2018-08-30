using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using System.Linq;
using Terraria.ModLoader.IO;  
 
namespace Decimation
{
    public class DecimationWorld : ModWorld
    {
		public static bool downedBloodshotEye = false;	
		public static bool downedDuneWorm = false;
        public static bool downedArachnus = false;
		public override void Initialize()
		{
			downedBloodshotEye = false;
			downedDuneWorm = false;
            downedArachnus = false;
		}
		public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedBloodshotEye) downed.Add("downedBloodshotEye");
			if (downedDuneWorm) downed.Add("downedDuneWorm");
            if (downedArachnus) downed.Add("downedArachnus");
            return new TagCompound	{
                {"downed", downed},
			};				
        }
		public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedBloodshotEye = downed.Contains("downedBloodshotEye");
			downedDuneWorm = downed.Contains("downedDuneWorm");
            downedArachnus = downed.Contains("downedArachnus");
        }
		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = downedBloodshotEye; //+1 flag number for each new boss
			flags[1] = downedDuneWorm;
            flags[2] = downedArachnus;
		}
		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedBloodshotEye = flags[0];
			downedDuneWorm = flags[1];
            downedArachnus = flags[2];
		}
        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 1)
            {
                byte flags = reader.ReadByte();
                DecimationWorld.downedBloodshotEye = ((flags & 1) != 0);
                DecimationWorld.downedDuneWorm = ((flags & 2) != 0); //double flag numbers with each new boss
                DecimationWorld.downedArachnus = ((flags & 4) != 0);
            }
            else if (loadVersion == 2)
            {
                byte flags = reader.ReadByte();
                byte flags2 = reader.ReadByte();
                DecimationWorld.downedBloodshotEye = ((flags & 1) != 0);
                DecimationWorld.downedDuneWorm = ((flags & 2) != 0);
                DecimationWorld.downedArachnus = ((flags & 4) != 0);
            }
        }

        // For custom biome
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            tasks.Insert(genIndex + 1, new PassLegacy("Dirt Blob", delegate (GenerationProgress progress)
            {
                WorldGen.TileRunner(Main.spawnTileX, Main.spawnTileY + -46, 6, Main.rand.Next(1, 3), TileID.Dirt, true, 0f, 0f, true, true);
            }));
        }
    }
}	
