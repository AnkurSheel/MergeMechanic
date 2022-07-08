using System.Collections.Generic;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileTracker : ITileTracker
    {
        private readonly List<ITile> _fullTiles = new List<ITile>();
        private readonly List<ITile> _emptyTiles = new List<ITile>();

        private static ITileTracker? _instance;

        //Todo: Get rid of this singleton and use DI instead with an pub/sub model  
        public static ITileTracker Instance => _instance ?? (_instance = new TileTracker());

        // Only needed for tests. Should be private

        public bool HasEmptyTile => _emptyTiles.Count > 0;

        public void OnTileCreated(ITile tile)
        {
            _emptyTiles.Add(tile);
        }

        public ITile GetEmptyTile()
        {
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
