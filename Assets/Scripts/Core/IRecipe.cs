namespace MergeMechanic.Core
{
    public interface IRecipe
    {
        int CurrentLevel { get; }

        int Type { get; }

        void IncrementLevel();

        void ResetLocalPosition();

        bool CanIncrementLevel();

        bool CanMerge(IRecipe otherRecipe);

        void Destroy();
    }
}
