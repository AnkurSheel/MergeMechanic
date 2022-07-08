using MergeMechanic.Core;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace MergeMechanic.Tests
{
    [TestFixture]
    public class BoardGeneratorTests
    {
        private Mock<ITile> _tile;
        private Mock<IGridHelper> _gridHelper;
        private Mock<IGameObjectWrapper> _gameObjectWrapper;
        private IBoardGenerator _boardGenerator;
        private Transform _parentTransform;
        private Vector3 _tileSize;
        private Mock<ITileTracker> _tileTracker;

        [SetUp]
        public void Setup()
        {
            _tile = new Mock<ITile>();
            _gridHelper = new Mock<IGridHelper>();
            _gameObjectWrapper = new Mock<IGameObjectWrapper>();
            _tileTracker = new Mock<ITileTracker>();

            _boardGenerator = new BoardGenerator(_gridHelper.Object, _gameObjectWrapper.Object, _tileTracker.Object);

            _tileSize = new Vector3(10, 10, 0);

            var parent = new GameObject
            {
                transform =
                {
                    position = new Vector3(10, 10, 10)
                }
            };
            _parentTransform = parent.transform;
        }

        [Test]
        public void A_tile_is_created_for_each_row_and_column()
        {
            var cell = new GameObject();
            _boardGenerator.CreateBoard(
                2,
                3,
                _tileSize,
                _parentTransform,
                cell,
                tile => _tile.Object);

            _gameObjectWrapper.Verify(
                x => x.Instantiate(
                    cell,
                    It.IsAny<Vector3>(),
                    It.IsAny<Transform>(),
                    It.IsAny<string>()),
                Times.Exactly(6));
        }

        [Test]
        public void All_tiles_are_have_the_correct_position_when_board_is_created()
        {
            _boardGenerator.CreateBoard(
                2,
                3,
                _tileSize,
                _parentTransform,
                new GameObject(),
                tile => _tile.Object);

            _gridHelper.Verify(
                x => x.GetTilePosition(
                    _parentTransform.position,
                    _tileSize,
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Exactly(6));
        }
    }
}
