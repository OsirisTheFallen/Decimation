using Decimation.Core.Items;

namespace Decimation.Items.Misc
{
    internal class BloodiedEssence : DecimationItem
    {
        protected override string ItemName => "Bloodied Essence";
        protected override string ItemTooltip => "It molds more and more as time resumes.";

        protected override void Init()
        {
            width = 16;
            height = 16;
            value = 100;

            this.item.maxStack = 999;
        }
    }
}