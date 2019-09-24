using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Decimation.Buffs.Buffs
{
    internal class NaturesAura : DecimationBuff
    {
        protected override string DisplayName => "Natures Aura";
        protected override string Description => "Nature strengthens your will and power.";

        protected override void Init()
        {
            save = false;
            displayTime = false;
            clearable = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 2;
        }
    }
}