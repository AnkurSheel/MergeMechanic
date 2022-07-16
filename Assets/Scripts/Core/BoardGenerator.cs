using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class BoardGenerator : IBoardGenerator
    {
        private readonly IGridHelper _gridHelper;

        private readonly IGameObjectWrapper _gameObjectWrapper;
        private readonly ITileTracker _tileTracker;

        private static IBoardGenerator? _instance;

        //Todo: Get rid of this singleton and use DI instead with an pub/sub model  
        public static IBoardGenerator Instance => _instance ?? (_instance = new BoardGenerator(new GridHelper(), new GameObjectWrapper(), TileTracker.Instance));

        // Needed to be public for tests
        public BoardGenerator(IGridHelper gridHelper, IGameObjectWrapper gameObjectWrapper, ITileTracker tileTracker)
        {
            _gridHelper = gridHelper;
            _gameObjectWrapper = gameObjectWrapper;
            _tileTracker = tileTracker;
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

        public void PopulateTile(GameObject gameObjectToGenerate, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var tile = _tileTracker.GetEmptyTile();

                if (tile == null)
                {
                    Debug.Log("No Available Spaces");
                }
                else
                {
                    _gameObjectWrapper.Instantiate(gameObjectToGenerate, tile.GetTransform());
                }
            }
        }
    }
}
