namespace Decimation.Core.Items
{
    public abstract class DecimationPlaceableItem : DecimationItem
    {
        protected abstract int Tile { get; }

        protected abstract void InitPlaceable();

        protected sealed override void Init()
        {
            width = 12;
            height = 12;
            autoReuse = true;
            useTime = 15;
            useAnimation = 15;
            useStyle = 1;
            consumable = true;

            this.item.useTurn = true;

            InitPlaceable();

            this.item.createTile = this.Tile;
        }
    }
}