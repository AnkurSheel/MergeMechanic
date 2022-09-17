using UnityEngine;

namespace MergeMechanic.Core.Camera
{
    public class CameraAdjuster : ICameraAdjuster
    {
        private readonly Vector2 _cameraPositionModifier = new Vector2(2.5f, 6.0f);
        private const float _cameraSizeModifier = 1.15f;
        private const int _minWidth = 2;
        private const float positionZ = -10.0f;

        public Vector3 GetAdjustedPosition(int boardWidth)
        {
            var widthDifference = boardWidth - _minWidth;
            return new Vector3(widthDifference * _cameraPositionModifier.x, widthDifference * _cameraPositionModifier.y, positionZ);
        }

        public float GetAdjustedOrthographicSize(int boardWidth)
        {
            var widthDifference = boardWidth - _minWidth;
            return Mathf.Pow(_cameraSizeModifier, widthDifference) * 20;
        }
    }
}
