using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decimation.Items.Placeable
{
    internal abstract class DecimationPlaceableItem : DecimationItem
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

            item.useTurn = true;

            InitPlaceable();

            item.createTile = Tile;
        }
    }
}
