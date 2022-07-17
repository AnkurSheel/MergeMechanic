using System;

namespace MergeMechanic.Core
{
    public interface ITileElement
    {
        void OnMerge(ITileElement triggeredTile, Func<int, bool> onMergeFunc);

        int Level { get; }

        bool IncrementLevel(Func<int, bool> instantiateGameObjectFunc);

        void ResetLocalPosition();
    }
}
