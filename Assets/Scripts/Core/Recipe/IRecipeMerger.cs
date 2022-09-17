using MergeMechanic.Core.Models;

namespace MergeMechanic.Core.Recipe
{
    public interface IRecipeMerger
    {
        bool OnMerge(IRecipe selectedRecipe, IRecipe triggeredRecipe, Tile selectedTile);
    }
}
