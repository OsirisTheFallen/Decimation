namespace Decimation.Core.Items
{
    public abstract class DecimationAccessory : DecimationItem
    {
        protected abstract void InitAccessory();

        protected sealed override void Init()
        {
            this.item.accessory = true;
            this.item.maxStack = 1;

            InitAccessory();
        }
    }
}