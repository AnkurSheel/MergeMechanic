using UnityEngine;

namespace MergeMechanic.Core
{
    public class GridHelper : IGridHelper
    {
        public Vector3 GetTilePosition(
            Vector3 startPosition,
            Vector3 tileSize,
            int column,
            int row)
            => new Vector3(startPosition.x + tileSize.x * column, startPosition.y + tileSize.y * row, 0);
    }
}
