using MergeMechanic.Core;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace MergeMechanic.Tests
{
    [TestFixture]
    public class TileElementTests
    {
        private Mock<IGameObjectWrapper> _gameObjectWrapper;
        private TileElement _tileElement;

        [SetUp]
        public void Setup()
        {
            _gameObjectWrapper = new Mock<IGameObjectWrapper>();
            _tileElement = new TileElement(
                _gameObjectWrapper.Object,
                Mock.Of<ITileTracker>());
        }

        [Test]
        public void Level_is_incremented_correctly_if_can_merge()
        {
            var numberOfIncrements = 3;

            for (var i = 0; i < numberOfIncrements; i++)
            {
                _tileElement.IncrementLevel(i => true);
            }

            Assert.AreEqual(numberOfIncrements + 1, _tileElement.Level);
        }

        [Test]
        public void Level_is_not_incremented_if_cannot_merge()
        {
            _tileElement.IncrementLevel(i => false);
            Assert.AreEqual(1, _tileElement.Level);
        }

        [Test]
        public void The_current_level_is_passed_to_the_callback_on_level_incrementation()
        {
            var submittedInput = 0;
            var numberOfIncrements = 3;

            for (var i = 0; i < numberOfIncrements; i++)
            {
                _tileElement.IncrementLevel(
                    i =>
                    {
                        submittedInput = i;
                        return true;
                    });
            }

            Assert.AreEqual(submittedInput, numberOfIncrements);
        }
    }
}
