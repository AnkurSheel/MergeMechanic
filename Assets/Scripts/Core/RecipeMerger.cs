using MergeMechanic.Core.Models;

namespace MergeMechanic.Core
{
    public class RecipeMerger : IRecipeMerger
    {
        private IRecipe _recipe;
        private readonly ITileTracker _tileTracker;

        public RecipeMerger(ITileTracker tileTracker)
        {
            _tileTracker = tileTracker;
        }

        public bool OnMerge(IRecipe selectedRecipe, IRecipe triggeredRecipe, Tile selectedTile)
        {
            if (CanMergeRecipes(selectedRecipe, triggeredRecipe))
            {
                triggeredRecipe.IncrementLevel();
                selectedRecipe.Destroy();
                _tileTracker.MakeTileEmpty(selectedTile);

                return true;
            }

            return false;
        }

        private bool CanMergeRecipes(IRecipe selectedRecipe, IRecipe triggeredRecipe)
            => triggeredRecipe.CanMerge(selectedRecipe) && triggeredRecipe.CanIncrementLevel();
    }
}
