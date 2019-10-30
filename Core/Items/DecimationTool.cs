namespace Decimation.Core.Items
{
    public abstract class DecimationTool : DecimationItem
    {
        protected virtual int MeleeDamages { get; }
        protected virtual int PickaxePower { get; }
        protected virtual int AxePower { get; }
        protected virtual int HammerPower { get; }

        protected abstract void InitTool();

        protected override void Init()
        {
            autoReuse = true;
            this.item.useTurn = true;
            this.item.maxStack = 1;

            InitTool();

            this.item.damage = this.MeleeDamages;
            this.item.pick = this.PickaxePower;
            this.item.axe = this.AxePower;
            this.item.hammer = this.HammerPower;

            if (this.MeleeDamages > 0) this.item.melee = true;
        }
    }
}