namespace MergeMechanic.Core
{
    public interface ITileTracker
    {
        bool HasEmptyTile { get; }

        void OnTileCreated(ITileElement tileElement);

        ITileElement PopulateRandomTile();

        void OnMerge(ITileElement tileElement);
    }
}
