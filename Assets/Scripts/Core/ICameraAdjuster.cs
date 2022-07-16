using UnityEngine;

namespace MergeMechanic.Core
{
    public interface ICameraAdjuster
    {
        Vector3 GetAdjustedPosition(int boardWidth);

        float GetAdjustedOrthographicSize(int boardWidth);
    }
}
