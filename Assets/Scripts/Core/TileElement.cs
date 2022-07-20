using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileElement : ITileElement
    {
        private bool _selected;
        private bool _collided;

        public int Level { get; private set; }

        private readonly GameObject _gameObject;
        private readonly ITile _tile;
        private readonly IGameObjectWrapper _gameObjectWrapper;
        private readonly ITileTracker _tileTracker;

        public TileElement(
            GameObject gameObject,
            ITile tile,
            IGameObjectWrapper gameObjectWrapper,
            ITileTracker tileTracker)
        {
            _gameObject = gameObject;
            _tile = tile;
            _gameObjectWrapper = gameObjectWrapper;
            _tileTracker = tileTracker;
            Level = 1;
        }

        public void OnMerge(ITileElement triggeredTile, Func<int, bool> onMergeFunc)
        {
            if (triggeredTile.Level == Level)
            {
                var canMerge = triggeredTile.IncrementLevel(onMergeFunc);

                if (canMerge)
                {
                    Level = 1;
                    _tileTracker.MakeTileEmpty(_tile);
                    _gameObjectWrapper.Destroy(_gameObject);
                }
            }
        }

        public bool IncrementLevel(Func<int, bool> onMergeFunc)
        {
            var canMerge = onMergeFunc(Level);

            if (canMerge)
            {
                Level++;
            }

            return canMerge;
        }

        public void ResetLocalPosition()
        {
            _gameObjectWrapper.ResetLocalPosition(_gameObject);
        }
    }
}
