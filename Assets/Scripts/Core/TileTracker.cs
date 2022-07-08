using System.Collections.Generic;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileTracker : ITileTracker
    {
        private readonly List<GameObject> _fullTiles = new List<GameObject>();
        private readonly List<GameObject> _emptyTiles = new List<GameObject>();

        private static ITileTracker? _instance;

        //Todo: Get rid of this singleton and use DI instead with an pub/sub model  
        public static ITileTracker Instance => _instance ?? (_instance = new TileTracker());

        // Only needed for tests. Should be private

        public bool HasEmptyTile => _emptyTiles.Count > 0;

        public void OnTileCreated(GameObject tile)
        {
            _emptyTiles.Add(tile);
        }

        public GameObject GetEmptyTile()
        {
            var tile = _emptyTiles[Random.Range(0, _emptyTiles.Count)];

            _fullTiles.Add(tile);
            _emptyTiles.Remove(tile);

            return tile;
        }

        public void MakeTileEmpty(GameObject tile)
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
