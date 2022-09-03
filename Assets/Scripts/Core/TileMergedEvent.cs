using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public class TileMergedEvent : IEvent
    {
        public ITileElement TileElement { get; }

        public ITileElement TriggeredTile { get; }

        public GameObject GameObject { get; }

        public ITile Tile { get; }

        public Func<int, bool> OnMergeFunc { get; }

        public TileMergedEvent(
            ITileElement tileElement,
            ITileElement triggeredTile,
            GameObject gameObject,
            ITile tile,
            Func<int, bool> onMergeFunc)
        {
            TileElement = tileElement;
            TriggeredTile = triggeredTile;
            GameObject = gameObject;
            Tile = tile;
            OnMergeFunc = onMergeFunc;
        }
    }
}
