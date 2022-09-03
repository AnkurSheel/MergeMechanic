using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public interface ITileElement
    {
        int Level { get; }

        bool IncrementLevel(Func<int, bool> instantiateGameObjectFunc);

        void ResetLocalPosition(GameObject gameObject);

        void ResetLevel();
    }
}
