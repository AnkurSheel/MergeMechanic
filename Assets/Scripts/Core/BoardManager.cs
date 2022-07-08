using UnityEngine;

namespace MergeMechanic.Core
{
    public class BoardManager : IBoardManager
    {
        private readonly IGridHelper _gridHelper;

        private readonly IGameObjectWrapper _gameObjectWrapper;
        private readonly ITileTracker _tileTracker;

        private static IBoardManager? _instance;

        //Todo: Get rid of this singleton and use DI instead with an pub/sub model  
        public static IBoardManager Instance => _instance ?? (_instance = new BoardManager(new GridHelper(), new GameObjectWrapper(), TileTracker.Instance));

        private BoardManager(IGridHelper gridHelper, IGameObjectWrapper gameObjectWrapper, ITileTracker tileTracker)
        {
            _gridHelper = gridHelper;
            _gameObjectWrapper = gameObjectWrapper;
            _tileTracker = tileTracker;
        }

        public void CreateBoard(
            int width,
            int height,
            Vector3 tileSize,
            Transform parentTransform,
            GameObject cell)
        {
            for (var column = 0; column < width; column++)
            {
                for (var row = 0; row < height; row++)
                {
                    var position = _gridHelper.GetTilePosition(
                        parentTransform.position,
                        tileSize,
                        column,
                        row);

                    var tile = _gameObjectWrapper.Instantiate(
                        cell,
                        position,
                        parentTransform,
                        $"Tile_r{row}_c{column}");

                    _tileTracker.OnTileCreated(tile);
                }
            }
        }

        public void PopulateTile(GameObject gameObjectToGenerate)
        {
            if (!_tileTracker.HasEmptyTile)
            {
                Debug.Log("Game Ended!");
            }
            else
            {
                var tile = _tileTracker.GetEmptyTile();
                var tileElement = _gameObjectWrapper.Instantiate(gameObjectToGenerate, tile.transform);
            }
        }
    }
}
