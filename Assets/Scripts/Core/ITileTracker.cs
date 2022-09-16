using UnityEngine;

namespace MergeMechanic.Core
{
    public interface ITileTracker
    {
        void AddEmptyTile(ITile tile);

        void MakeTileEmpty(ITile tile);

        bool PopulateTile(GameObject gameObjectToGenerate, int amount);
    }
}
