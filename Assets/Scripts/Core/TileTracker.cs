using System.Collections.Generic;
using MergeMechanic.Core.Models;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileTracker : ITileTracker
    {
        private readonly List<Tile> _fullTiles = new List<Tile>();
        private readonly List<Tile> _emptyTiles = new List<Tile>();

        private readonly IGameObjectWrapper _gameObjectWrapper;

        public TileTracker(IGameObjectWrapper gameObjectWrapper)
        {
            _gameObjectWrapper = gameObjectWrapper;
        }

        public void AddEmptyTile(Tile tile)
        {
            _emptyTiles.Add(tile);
        }

        public void MakeTileEmpty(Tile tile)
        {
            var tileToRemove = _fullTiles.Find(x => x == tile);

            if (tileToRemove != null)
            {
                _emptyTiles.Add(tileToRemove);
                _fullTiles.Remove(tileToRemove);
            }
            else
            {
                Debug.LogError("Trying to merge a tile element that was not populated");
            }
        }

        public bool PopulateTile(GameObject gameObjectToGenerate, int amount)
        {
            if (_emptyTiles.Count == 0)
            {
                Debug.Log("No Available Spaces");
                return false;
            }

            for (var i = 0; i < amount; i++)
            {
                var tile = GetEmptyTile();

                if (tile == null)
                {
                    break;
                }

                _gameObjectWrapper.Instantiate(gameObjectToGenerate, tile.GetTransform());
            }

            return true;
        }

        private Tile? GetEmptyTile()
        {
            if (_emptyTiles.Count == 0)
            {
                return null;
            }

            var tile = _emptyTiles[Random.Range(0, _emptyTiles.Count)];

            _fullTiles.Add(tile);
            _emptyTiles.Remove(tile);

            return tile;
        }
    }
}
