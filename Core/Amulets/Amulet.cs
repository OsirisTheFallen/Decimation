using System.Collections.Generic;
using Decimation.Core.Amulets.Synergy;
using Decimation.Core.Collections;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ModLoader;

namespace Decimation.Core.Amulets
{
    public abstract class Amulet : DecimationItem
    {
        public abstract AmuletClasses AmuletClass { get; }
        public virtual IAmuletsSynergy Synergy { get; } = new AmuletSynergyAdapter();

        protected abstract void GetAmuletTooltip(ref AmuletTooltip tooltip);

        protected virtual void InitAmulet() { }
        protected virtual void UpdateAmulet(Player player)
        {
        }

        protected sealed override void Init()
        {
            width = 28;
            height = 30;
            rarity = Rarity.Green;
            this.item.maxStack = 1;

            InitAmulet();

            AmuletList.Instance.Add(this);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            UpdateAmulet(Main.LocalPlayer);
        }

        public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            AmuletTooltip tooltip = new AmuletTooltip(this.mod, this);
            GetAmuletTooltip(ref tooltip);

            tooltips.AddRange(tooltip.Lines);
        }
    }

    public enum AmuletClasses
    {
        Melee,
        Summoner,
        Mage,
        Ranger,
        Throwing,
        Tank,
        Healer,
        Builder,
        Miner,
        Creator
    }
}