using UnityEngine;

namespace MergeMechanic.Core.Board
{
    public class BoardGenerator : IBoardGenerator
    {
        private readonly IGridHelper _gridHelper;

        private readonly IGameObjectWrapper _gameObjectWrapper;

        // Needed to be public for tests
        public BoardGenerator(IGridHelper gridHelper, IGameObjectWrapper gameObjectWrapper)
        {
            _gridHelper = gridHelper;
            _gameObjectWrapper = gameObjectWrapper;
        }

        public void CreateBoard(
            int width,
            Vector3 tileSize,
            Transform parentTransform,
            GameObject cell)
        {
            for (var column = 0; column < width; column++)
            {
                for (var row = 0; row < width; row++)
                {
                    var position = _gridHelper.GetTilePosition(
                        parentTransform.position,
                        tileSize,
                        column,
                        row);

                    _gameObjectWrapper.Instantiate(
                        cell,
                        position,
                        parentTransform,
                        $"Tile_r{row}_c{column}");
                }
            }
        }
    }
}
