namespace MergeMechanic.Core
{
    public interface ITileTracker
    {
        void OnTileCreated(ITile tile);

        ITile? GetEmptyTile();

        void MakeTileEmpty(ITile tile);
    }
}
