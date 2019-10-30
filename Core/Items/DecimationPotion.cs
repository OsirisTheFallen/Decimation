namespace Decimation.Core.Items
{
    public abstract class DecimationPotion : DecimationItem
    {
        protected virtual int HealLife { get; } = 0;
        protected virtual int HealMana { get; } = 0;
        protected abstract int BuffType { get; }
        protected abstract int BuffTime { get; }

        protected abstract void InitPotion();

        protected sealed override void Init()
        {
            width = 20;
            height = 20;
            consumable = true;
            useTime = 20;
            useAnimation = 20;

            item.maxStack = 30;
            item.useTurn = true;
            item.useStyle = 2;

            InitPotion();

            item.healLife = HealLife;
            item.healMana = HealMana;
            item.buffType = BuffType;
            item.buffTime = BuffTime;

            if (HealLife > 0)
            {
                item.potion = true;
            }
        }
    }
}
