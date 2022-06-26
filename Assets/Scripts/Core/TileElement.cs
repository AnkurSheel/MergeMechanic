using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileElement
    {
        private bool _selected;
        private bool _collided;

        public int Level { get; private set; }

        private readonly GameObject _gameObject;

        public TileElement(GameObject gameObject)
        {
            _gameObject = gameObject;
            Level = 1;
        }

        public void OnMerge(TileElement triggeredTile, Action<int> onMergeFunc)
        {
            if (triggeredTile.Level == Level)
            {
                triggeredTile.IncrementLevel(onMergeFunc);
                Level = 1;
                BoardManager.Instance.OnMerge(this);
                _gameObject.SetActive(false);
            }
        }

        public void IncrementLevel(Action<int> onMergeFunc)
        {
            onMergeFunc(Level);
            Level++;
        }

        public void Show()
        {
            _gameObject.SetActive(true);
        }

        public void Hide()
        {
            _gameObject.SetActive(false);
        }
    }
}
