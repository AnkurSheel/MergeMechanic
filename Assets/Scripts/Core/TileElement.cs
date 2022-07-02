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
        private readonly IGameObjectWrapper _gameObjectWrapper;

        public TileElement(GameObject gameObject, IGameObjectWrapper gameObjectWrapper)
        {
            _gameObject = gameObject;
            _gameObjectWrapper = gameObjectWrapper;
            Level = 1;
        }

        public void OnMerge(ITileElement triggeredTile, Action<int> onMergeFunc)
        {
            if (triggeredTile.Level == Level)
            {
                triggeredTile.IncrementLevel(onMergeFunc);
                Level = 1;
                TileTracker.Instance.OnMerge(this);
                _gameObjectWrapper.SetActive(_gameObject, false);
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

        public void Show()
        {
            _gameObjectWrapper.SetActive(_gameObject, true);
        }

        public void Hide()
        {
            _gameObjectWrapper.SetActive(_gameObject, false);
        }
    }
}
