using MergeMechanic.Core;
using MergeMechanic.Core.Models;
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
        private Mock<IGameObjectWrapper> _gameObjectWrapper;

        [SetUp]
        public void Setup()
        {
            _gameObjectWrapper = new Mock<IGameObjectWrapper>();
            _tileTracker = new TileTracker(_gameObjectWrapper.Object);
        }

        [Test]
        public void If_no_tiles_are_created_cannot_populate_a_tile()
        {
            Assert.False(PopulateTile());
            VerifyCallToGameObjectWrapper(Times.Exactly(0));
        }

        [Test]
        public void If_a_tile_is_created_can_populate_an_tile()
        {
            AddEmptyTile();

            Assert.True(PopulateTile());
            VerifyCallToGameObjectWrapper(Times.Exactly(1));
        }

        [Test]
        public void If_all_tiles_are_populated_cannot_populate_an_tile()
        {
            AddPopulatedTile();

            Assert.False(PopulateTile());
        }

        [Test]
        public void If_all_tiles_are_populated_and_a_tile_is_merged_can_populate_an_tile()
        {
            var tile = AddPopulatedTile();
            _tileTracker.MakeTileEmpty(tile);

            Assert.True(PopulateTile());
        }

        [Test]
        public void If_trying_to_populate_more_tile_than_available_it_populates_all_available_spaces()
        {
            AddEmptyTile();
            AddEmptyTile();
            AddPopulatedTile();

            _gameObjectWrapper.Invocations.Clear();

            Assert.True(PopulateTile(2));
            VerifyCallToGameObjectWrapper(Times.Exactly(2));
        }

        [Test]
        public void Making_a_tile_empty_that_has_not_been_populated_logs_error()
        {
            var tile = AddEmptyTile();
            _tileTracker.MakeTileEmpty(tile);

            LogAssert.Expect(LogType.Error, "Trying to merge a tile element that was not populated");
        }

        private bool PopulateTile(int amount = 1)
            => _tileTracker.PopulateTile(new GameObject(), amount);

        private Tile AddEmptyTile()
        {
            var tile = new Tile(new GameObject());
            _tileTracker.AddEmptyTile(tile);
            return tile;
        }

        private Tile AddPopulatedTile()
        {
            var tile = AddEmptyTile();
            PopulateTile();
            return tile;
        }

        private void VerifyCallToGameObjectWrapper(Times times)
        {
            _gameObjectWrapper.Verify(x => x.Instantiate(It.IsAny<GameObject>(), It.IsAny<Transform>()), times);
        }
    }
}
