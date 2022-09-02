using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileElement : ITileElement, IEventListener<TileMergedEvent>
    {
        private bool _selected;
        private bool _collided;

        public int Level { get; private set; }

        private readonly IGameObjectWrapper _gameObjectWrapper;
        private readonly ITileTracker _tileTracker;

        // remove tile tracker as a dependency
        public TileElement(IGameObjectWrapper gameObjectWrapper, ITileTracker tileTracker)
        {
            _gameObjectWrapper = gameObjectWrapper;
            _tileTracker = tileTracker;
            Level = 1;
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

        public void ResetLocalPosition(GameObject gameObject)
        {
            _gameObjectWrapper.ResetLocalPosition(gameObject);
        }

        public void ResetLevel()
        {
            Level = 1;
        }

        // move this
        public EventListenerStatus OnEvent(TileMergedEvent input)
        {
            if (input.TriggeredTile.Level == input.TileElement.Level)
            {
                var canMerge = input.TriggeredTile.IncrementLevel(input.OnMergeFunc);

                if (canMerge)
                {
                    input.TileElement.ResetLevel();
                    _tileTracker.MakeTileEmpty(input.Tile);
                    _gameObjectWrapper.Destroy(input.GameObject);
                }
            }

            return EventListenerStatus.Success;
        }
    }
}
