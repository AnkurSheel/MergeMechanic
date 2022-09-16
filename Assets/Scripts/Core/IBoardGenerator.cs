using System;
using UnityEngine;

namespace MergeMechanic.Core
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
