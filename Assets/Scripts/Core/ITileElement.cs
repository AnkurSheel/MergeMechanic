using System;

namespace MergeMechanic.Core
{
    public interface ITileElement
    {
        void OnMerge(ITileElement triggeredTile, Action<int> onMergeFunc);

        void Show();

        void Hide();

        int Level { get; }

        void IncrementLevel(Action<int> instantiateGameObjectFunc);

        void ResetLocalPosition();
    }
}
