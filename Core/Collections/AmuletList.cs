using System;
using System.Collections.Generic;
using System.Linq;
using Decimation.Core.Amulets;
using Terraria;

namespace Decimation.Core.Collections
{
    public class AmuletList : List<Amulet>
    {

        private static readonly AmuletList _instance = new AmuletList();
        public static AmuletList Instance { get; } = _instance;
        private AmuletList() { }

        public Amulet GetAmuletForItem(Item item)
        {
            return this.FirstOrDefault(amulet => amulet.item.type == item.type);
        }

        public bool Contains(Item item)
        {
            return GetAmuletForItem(item) != null;
        }

    }
}
