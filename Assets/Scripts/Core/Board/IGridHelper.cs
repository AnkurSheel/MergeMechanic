using UnityEngine;

namespace MergeMechanic.Core.Board
{
    public interface IGridHelper
    {
        Vector3 GetTilePosition(
            Vector3 startPosition,
            Vector3 tileSize,
            int column,
            int row);
    }
}
