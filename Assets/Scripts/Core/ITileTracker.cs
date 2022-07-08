namespace MergeMechanic.Core
{
    public interface ITileTracker
    {
        bool HasEmptyTile { get; }

        void OnTileCreated(ITile tile);

        ITile GetEmptyTile();

        void MakeTileEmpty(ITile tile);
    }
}
