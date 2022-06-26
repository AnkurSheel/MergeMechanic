using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MergeMechanic
{
    public class BoardManager
    {
        private readonly List<TileElementMonoBehaviour> _fullTiles = new List<TileElementMonoBehaviour>();

        private readonly List<TileElementMonoBehaviour> _emptyTiles = new List<TileElementMonoBehaviour>();

        private static BoardManager _instance;

        public static BoardManager Instance => _instance ?? (_instance = new BoardManager());

        public void CreateBoard(
            int width,
            int height,
            Vector3 startPosition,
            Vector3 tileSize,
            Func<Vector3, TileElementMonoBehaviour> instantiateGameObjectFunc)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var tileElement = instantiateGameObjectFunc(new Vector3(startPosition.x + tileSize.x * i, startPosition.y + tileSize.y * j, 0));
                    tileElement.Hide();
                    _emptyTiles.Add(tileElement);
                }
            }
        }

        public void CreateNewTile()
        {
            if (_emptyTiles.Count == 0)
            {
                Debug.Log("Game Ended!");
            }
            else
            {
                var tileElement = _emptyTiles[Random.Range(0, _emptyTiles.Count)];
                tileElement.Show();

                _fullTiles.Add(tileElement);
                _emptyTiles.Remove(tileElement);
            }
        }

        public void OnMerge(TileElementMonoBehaviour tileElement)
        {
            var tile = _fullTiles.Find(x => x == tileElement);
            _emptyTiles.Add(tile);
            _fullTiles.Remove(tile);
            tileElement.gameObject.SetActive(false);
        }
    }
}
