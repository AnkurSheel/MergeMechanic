using System.Collections.Generic;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileTracker : ITileTracker
    {
        private readonly List<ITileElement> _fullTiles = new List<ITileElement>();
        private readonly List<ITileElement> _emptyTiles = new List<ITileElement>();

        private static ITileTracker? _instance;

        //Todo: Get rid of this singleton and use DI instead with an pub/sub model  
        public static ITileTracker Instance => _instance ?? (_instance = new TileTracker());

        // Only needed for tests. Should be private

        public bool HasEmptyTile => _emptyTiles.Count > 0;

        public void OnTileCreated(ITileElement tileElement)
        {
            _emptyTiles.Add(tileElement);
        }

        public ITileElement PopulateRandomTile()
        {
            var tileElement = _emptyTiles[Random.Range(0, _emptyTiles.Count)];

            _fullTiles.Add(tileElement);
            _emptyTiles.Remove(tileElement);

            return tileElement;
        }

        public void OnMerge(ITileElement tileElement)
        {
            var tile = _fullTiles.Find(x => x == tileElement);

            if (tile != null)
            {
                _emptyTiles.Add(tile);
                _fullTiles.Remove(tile);
            }
            else
            {
                Debug.LogError("Trying to merge a tile element that was not populated");
            }
        }
    }
}
