using System.Collections.Generic;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileTracker : ITileTracker
    {
        private readonly List<ITile> _fullTiles = new List<ITile>();
        private readonly List<ITile> _emptyTiles = new List<ITile>();

        public void OnTileCreated(ITile tile)
        {
            _emptyTiles.Add(tile);
        }

        public ITile? GetEmptyTile()
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

        public void MakeTileEmpty(ITile tile)
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
    }
}
