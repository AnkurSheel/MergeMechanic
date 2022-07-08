using UnityEngine;

namespace MergeMechanic.Core
{
    public interface IBoardManager
    {
        void CreateBoard(
            int width,
            int height,
            Vector3 tileSize,
            Transform parentTransform,
            GameObject cell);

        void PopulateTile(GameObject gameObjectToGenerate);
    }
}
