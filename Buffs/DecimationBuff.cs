using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace Decimation.Buffs
{
    internal abstract class DecimationBuff : ModBuff
    {
        protected bool save = false;
        protected bool displayTime = true;
        protected bool clearable = true;

        protected new abstract string DisplayName { get; }
        protected new abstract string Description { get; }
        public virtual bool Debuff { get; } = false;

        protected abstract void Init();

        public sealed override void SetDefaults()
        {
            //base.DisplayName.SetDefault(DisplayName);
            //base.Description.SetDefault(Description);

            //Main.debuff[Type] = Debuff;
            //Main.buffNoSave[Type] = !save;
            //Main.buffNoTimeDisplay[Type] = !displayTime;
            //canBeCleared = clearable;
        }
    }
}
