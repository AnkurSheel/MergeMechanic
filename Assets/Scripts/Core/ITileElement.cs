using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public interface ITileElement
    {
        void OnMerge(ITileElement triggeredTile, GameObject gameObject, Func<int, bool> onMergeFunc);

        int Level { get; }

        bool IncrementLevel(Func<int, bool> instantiateGameObjectFunc);

        void ResetLocalPosition(GameObject gameObject);
    }
}
