using System.Collections.Generic;
using Terraria.ModLoader;

namespace Decimation.Core.Util.Builder
{
    internal class RecipeBuilder
    {
        private readonly IDictionary<int, int> _ingredients = new Dictionary<int, int>();
        private readonly Mod _mod;
        private readonly int _result;
        private readonly int _resultQuantity;
        private readonly IList<int> _tiles = new List<int>();
        private bool _anyIronBar;

        public RecipeBuilder(Mod mod, ModItem result, int resultQuantity = 1)
        {
            _mod = mod;
            _result = result.item.type;
            _resultQuantity = resultQuantity;
        }

        public RecipeBuilder WithIngredient(int item, int quantity = 1)
        {
            _ingredients.Add(item, quantity);

            return this;
        }

        public RecipeBuilder WithStation(int tile)
        {
            _tiles.Add(tile);

            return this;
        }

        public RecipeBuilder AnyIronBar(bool anyIronBar)
        {
            _anyIronBar = anyIronBar;

            return this;
        }

        public ModRecipe Build()
        {
            ModRecipe recipe = new ModRecipe(_mod) {anyIronBar = _anyIronBar};

            foreach (KeyValuePair<int, int> ingredient in _ingredients)
                recipe.AddIngredient(ingredient.Key, ingredient.Value);

            foreach (int tile in _tiles) recipe.AddTile(tile);

            recipe.SetResult(_result, _resultQuantity);

            return recipe;
        }
    }
}