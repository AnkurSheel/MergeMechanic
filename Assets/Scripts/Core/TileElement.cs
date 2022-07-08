using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MergeMechanic.Core
{
    public class TileElement : ITileElement
    {
        private bool _selected;
        private bool _collided;

        public int Level { get; private set; }

        private readonly GameObject _gameObject;
        private readonly IGameObjectWrapper _gameObjectWrapper;
        private readonly ITileTracker _tileTracker;

        public TileElement(GameObject gameObject, IGameObjectWrapper gameObjectWrapper, ITileTracker tileTracker)
        {
            _gameObject = gameObject;
            _gameObjectWrapper = gameObjectWrapper;
            _tileTracker = tileTracker;
            Level = 1;
        }

        public void OnMerge(ITileElement triggeredTile, Action<int> onMergeFunc)
        {
            if (triggeredTile.Level == Level)
            {
                triggeredTile.IncrementLevel(onMergeFunc);
                Level = 1;
                _tileTracker.MakeTileEmpty(_gameObject.transform.parent.gameObject);
                Object.Destroy(_gameObject);
            }
        }

        public void IncrementLevel(Action<int> onMergeFunc)
        {
            onMergeFunc(Level);
            Level++;
        }

        public void ResetLocalPosition()
        {
            _gameObjectWrapper.ResetLocalPosition(_gameObject);
        }
    }
}
