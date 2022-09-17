using MergeMechanic.Core.Models;
using UnityEngine;

namespace MergeMechanic.Core
{
    public interface ITileTracker
    {
        void AddEmptyTile(Tile tile);

        void MakeTileEmpty(Tile tile);

        bool PopulateTile(GameObject gameObjectToGenerate, int amount);
    }
}
