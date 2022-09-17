using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class Recipe : IRecipe
    {
        private readonly int _maxLevel;
        private readonly GameObject _gameObject;
        private readonly IGameObjectWrapper _gameObjectWrapper;

        public int Type { get; }

        public int CurrentLevel { get; private set; }

        public Recipe(
            int type,
            int maxLevel,
            GameObject gameObject,
            IGameObjectWrapper gameObjectWrapper)
        {
            Type = type;
            _gameObjectWrapper = gameObjectWrapper;
            _maxLevel = maxLevel;
            _gameObject = gameObject;
            ResetLevel();
        }

        public void IncrementLevel()
        {
            CurrentLevel = Math.Min(CurrentLevel + 1, _maxLevel);
        }

        public void ResetLocalPosition()
        {
            _gameObjectWrapper.ResetLocalPosition(_gameObject);
        }

        public bool CanIncrementLevel()
            => CurrentLevel < _maxLevel;

        public bool CanMerge(IRecipe otherRecipe)
            => CurrentLevel == otherRecipe.CurrentLevel && Type == otherRecipe.Type;

        public void Destroy()
        {
            ResetLevel();
            _gameObjectWrapper.Destroy(_gameObject);
        }

        private void ResetLevel()
        {
            CurrentLevel = 1;
        }
    }
}
