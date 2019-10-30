using System.Collections.Generic;
using Decimation.Core.Util;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Core.Items
{
    public abstract class DecimationItem : ModItem
    {
        protected bool autoReuse = false;
        protected bool consumable = false;
        protected int height = 16;
        protected Rarity rarity = Rarity.White;
        protected int useAnimation = 20;
        protected LegacySoundStyle useSound = SoundID.Item1;
        protected int useStyle = 1;
        protected int useTime = 20;
        protected int value = 1;

        protected int width = 16;
        protected abstract string ItemName { get; }
        protected virtual string ItemTooltip { get; } = "";
        protected virtual DrawAnimation Animation { get; } = null;
        protected virtual bool IsClone { get; } = false;

        public sealed override void SetStaticDefaults()
        {
            this.DisplayName.SetDefault(this.ItemName);
            this.Tooltip.SetDefault(this.ItemTooltip);

            if (this.Animation != null) Main.RegisterItemAnimation(this.item.type, this.Animation);
        }

        public sealed override void SetDefaults()
        {
            this.item.maxStack = 999;
            this.item.noMelee = true;

            Init();

            if (!this.IsClone)
            {
                this.item.width = width;
                this.item.height = height;
                this.item.rare = (int) rarity;
                this.item.consumable = consumable;
                this.item.autoReuse = autoReuse;
                this.item.useStyle = useStyle;
                this.item.useTime = useTime;
                this.item.useAnimation = useAnimation;
                this.item.UseSound = useSound;
                this.item.value = value;
            }
        }

        public sealed override void AddRecipes()
        {
            List<ModRecipe> recipes = GetAdditionalRecipes();
            recipes.Add(GetRecipe());

            foreach (ModRecipe recipe in recipes) recipe?.AddRecipe();
        }

        protected abstract void Init();

        protected virtual ModRecipe GetRecipe()
        {
            return null;
        }

        protected virtual List<ModRecipe> GetAdditionalRecipes()
        {
            return new List<ModRecipe>();
        }

        protected static ModRecipe GetNewModRecipe(DecimationItem result, int quantity, int tile,
            bool anyIronBar = false)
        {
            return GetNewModRecipe(result, quantity, new List<int> {tile}, anyIronBar);
        }

        protected static ModRecipe GetNewModRecipe(DecimationItem result, int quantity, List<int> tiles,
            bool anyIronBar = false)
        {
            ModRecipe recipe = new ModRecipe(References.mod) {anyIronBar = anyIronBar};

            recipe.SetResult(result, quantity);

            foreach (int tile in tiles) recipe.AddTile(tile);

            return recipe;
        }
    }
}