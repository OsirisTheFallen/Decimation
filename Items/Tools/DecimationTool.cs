namespace Decimation.Items.Tools
{
    internal abstract class DecimationTool : DecimationItem
    {
        protected virtual int MeleeDamages { get; }
        protected virtual int PickaxePower { get; }
        protected virtual int AxePower { get; }
        protected virtual int HammerPower { get; }

        protected abstract void InitTool();

        protected override void Init()
        {
            autoReuse = true;
            item.useTurn = true;
            item.maxStack = 1;

            InitTool();

            item.damage = MeleeDamages;
            item.pick = PickaxePower;
            item.axe = AxePower;
            item.hammer = HammerPower;

            if (MeleeDamages > 0)
            {
                item.melee = true;
            }
        }
    }
}
