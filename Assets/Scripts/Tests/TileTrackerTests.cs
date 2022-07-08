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
            Assert.IsFalse(_tileTracker.HasEmptyTile);
        }

        [Test]
        public void If_a_tile_is_created_it_is_added_to_the_empty_collection()
        {
            _tileTracker.OnTileCreated(Mock.Of<ITile>());
            Assert.IsTrue(_tileTracker.HasEmptyTile);
        }

        [Test]
        public void If_a_tile_is_populated_it_is_removed_from_the_empty_collection()
        {
            _tileTracker.OnTileCreated(Mock.Of<ITile>());
            _tileTracker.GetEmptyTile();
            Assert.IsFalse(_tileTracker.HasEmptyTile);
        }

        [Test]
        public void If_a_tile_is_merged_it_is_added_to_the_empty_collection()
        {
            var tile = Mock.Of<ITile>();
            _tileTracker.OnTileCreated(tile);
            _tileTracker.GetEmptyTile();
            _tileTracker.MakeTileEmpty(tile);

            Assert.IsTrue(_tileTracker.HasEmptyTile);
        }

        [Test]
        public void If_a_tile_that_has_not_been_populated_is_merged_it_is_not_added_to_the_empty_collection1()
        {
            var tile = Mock.Of<ITile>();
            _tileTracker.OnTileCreated(tile);
            _tileTracker.GetEmptyTile();
            _tileTracker.MakeTileEmpty(Mock.Of<ITile>());
            
            LogAssert.Expect(LogType.Error, "Trying to merge a tile element that was not populated");
            Assert.IsFalse(_tileTracker.HasEmptyTile);
        }
    }
}
