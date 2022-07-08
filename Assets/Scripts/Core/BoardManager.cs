using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class BoardManager : IBoardManager
    {
        private readonly IGridHelper _gridHelper;

        private readonly IGameObjectWrapper _gameObjectWrapper;
        private readonly ITileTracker _tileTracker;

        public BoardManager(IGridHelper gridHelper, IGameObjectWrapper gameObjectWrapper, ITileTracker tileTracker)
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
            GameObject cell,
            Func<GameObject, ITileElement> getTileElement)
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

                    var tileElement = getTileElement(tile);

                    tileElement.Hide();
                    _tileTracker.OnTileCreated(tileElement);
                }
            }
        }

        public void PopulateTile()
        {
            if (!TileTracker.Instance.HasEmptyTile)
            {
                Debug.Log("Game Ended!");
            }
            else
            {
                var tileElement = _tileTracker.PopulateRandomTile();
                tileElement.Show();
            }
        }
    }
}
