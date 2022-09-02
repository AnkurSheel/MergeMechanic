using MergeMechanic.Core;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MergeMechanic.Tests
{
    [TestFixture]
    public class TileTrackerTests
    {
        private ITileTracker _tileTracker;

        [SetUp]
        public void Setup()
        {
            _tileTracker = new TileTracker();
        }

        [Test]
        public void If_no_tiles_are_created_it_has_no_empty_tiles()
        {
            Assert.Null(_tileTracker.GetEmptyTile());
        }

        [Test]
        public void If_a_tile_is_created_can_get_an_empty_tile()
        {
            _tileTracker.AddEmptyTile(Mock.Of<ITile>());
            Assert.NotNull(_tileTracker.GetEmptyTile());
        }

        [Test]
        public void If_all_tiles_are_populated_cannot_get_an_empty_tile()
        {
            _tileTracker.AddEmptyTile(Mock.Of<ITile>());
            _tileTracker.GetEmptyTile();
            Assert.Null(_tileTracker.GetEmptyTile());
        }

        [Test]
        public void If_all_tiles_are_populated_and_a_tile_is_merged_can_get_an_empty_tile()
        {
            var tile = Mock.Of<ITile>();
            _tileTracker.AddEmptyTile(tile);
            _tileTracker.GetEmptyTile();
            _tileTracker.MakeTileEmpty(tile);

            Assert.NotNull(_tileTracker.GetEmptyTile());
        }

        [Test]
        public void If_a_tile_that_has_not_been_populated_is_merged_logs_error()
        {
            var tile = Mock.Of<ITile>();
            _tileTracker.AddEmptyTile(tile);
            _tileTracker.GetEmptyTile();
            _tileTracker.MakeTileEmpty(Mock.Of<ITile>());
            
            LogAssert.Expect(LogType.Error, "Trying to merge a tile element that was not populated");
            Assert.Null(_tileTracker.GetEmptyTile());
        }
    }
}
