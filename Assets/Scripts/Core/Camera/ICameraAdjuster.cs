using UnityEngine;

namespace MergeMechanic.Core.Camera
{
    public interface ICameraAdjuster
    {
        Vector3 GetAdjustedPosition(int boardWidth);

        float GetAdjustedOrthographicSize(int boardWidth);
    }
}
