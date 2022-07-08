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
                new GameObject(),
                Mock.Of<ITile>(),
                _gameObjectWrapper.Object,
                Mock.Of<ITileTracker>());
        }

        [Test]
        public void Level_is_incremented_correctly()
        {
            var numberOfIncrements = 3;

            for (var i = 0; i < numberOfIncrements; i++)
            {
                _tileElement.IncrementLevel(i => { });
            }

            Assert.AreEqual(numberOfIncrements + 1, _tileElement.Level);
        }

        [Test]
        public void The_current_level_is_passed_to_the_callback_on_level_incrementation()
        {
            var submittedInput = 0;
            var numberOfIncrements = 3;

            for (var i = 0; i < numberOfIncrements; i++)
            {
                _tileElement.IncrementLevel(i => submittedInput = i);
            }

            Assert.AreEqual(submittedInput, numberOfIncrements);
        }
    }
}
