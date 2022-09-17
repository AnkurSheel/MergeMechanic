using UnityEngine;

namespace MergeMechanic.Core.Board
{
    public interface IBoardGenerator
    {
        void CreateBoard(
            int width,
            Vector3 tileSize,
            Transform parentTransform,
            GameObject cell);
    }
}
