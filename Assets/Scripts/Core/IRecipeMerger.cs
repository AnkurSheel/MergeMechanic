using MergeMechanic.Core.Models;

namespace MergeMechanic.Core
{
    public interface IRecipeMerger
    {
        bool OnMerge(IRecipe selectedRecipe, IRecipe triggeredRecipe, Tile selectedTile);
    }
}
