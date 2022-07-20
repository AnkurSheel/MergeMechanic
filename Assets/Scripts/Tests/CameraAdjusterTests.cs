using MergeMechanic.Core;
using NUnit.Framework;
using UnityEngine;

namespace MergeMechanic.Tests
{
    [TestFixture]
    public class CameraAdjusterTests
    {
        private ICameraAdjuster _cameraAdjuster;

        [SetUp]
        public void Setup()
        {
            _cameraAdjuster = new CameraAdjuster();
        }

        [TestCaseSource(nameof(TilePositionCases))]
        public void The_adjusted_position_is_calculated_correctly(int width, Vector3 expectedPosition)
        {
            var position = _cameraAdjuster.GetAdjustedPosition(width);
            Assert.AreEqual(expectedPosition, position);
        }

        [Test]
        [TestCase(3, 23)]
        [TestCase(9, 53.2f)]
        public void The_adjusted_orthographic_size_is_calculated_correctly(int width, float expectedOrthographicSize)
        {
            var orthographicSize = _cameraAdjuster.GetAdjustedOrthographicSize(width);
            Assert.AreEqual(expectedOrthographicSize, orthographicSize, 0.01f);
        }

        private static object[] TilePositionCases =
        {
            new object[] { 3, new Vector3(2.5f, 6, -10) },
            new object[] { 9, new Vector3(17.5f, 42, -10) }
        };
    }
}
