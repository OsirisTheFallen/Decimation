using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Decimation.Buffs.Buffs
{
    internal class FatesSmile : DecimationBuff
    {
        protected override string DisplayName => "Fate's Smile";
        protected override string Description => "Provide a slight life regeneration boost.";

        protected override void Init()
        {
            save = false;
            displayTime = false;
            clearable = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 1;
        }
    }
}