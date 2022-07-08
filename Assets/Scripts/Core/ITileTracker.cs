using UnityEngine;

namespace MergeMechanic.Core
{
    public interface ITileTracker
    {
        bool HasEmptyTile { get; }

        void OnTileCreated(GameObject tile);

        GameObject GetEmptyTile();

        void OnMerge(ITileElement tileElement);
    }
}
