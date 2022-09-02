namespace MergeMechanic.Core
{
    public interface ITileTracker
    {
        void AddEmptyTile(ITile tile);

        ITile? GetEmptyTile();

        void MakeTileEmpty(ITile tile);
    }
}
