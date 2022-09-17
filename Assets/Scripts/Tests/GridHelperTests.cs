using MergeMechanic.Core.Board;
using NUnit.Framework;
using UnityEngine;

namespace MergeMechanic.Tests
{
    public class GridHelperTests
    {
        [TestCaseSource(nameof(TilePositionCases))]
        public void Can_get_the_correct_position_for_tile(int row, int column, Vector3 expectedPosition)
        {
            var gridHelper = new GridHelper();

            var tilePosition = gridHelper.GetTilePosition(
                new Vector3(20, 20, 20),
                new Vector3(10, 10, 10),
                column,
                row);

            Assert.AreEqual(expectedPosition, tilePosition);
        }

        private static object[] TilePositionCases =
        {
            new object[] { 1, 1, new Vector3(30, 30, 0) },
            new object[] { 1, 2, new Vector3(40, 30, 0) },
            new object[] { 2, 1, new Vector3(30, 40, 0) },
            new object[] { 2, 2, new Vector3(40, 40, 0) },
        };
    }
}
