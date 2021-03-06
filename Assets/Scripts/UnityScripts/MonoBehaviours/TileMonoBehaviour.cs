using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.UnityScripts.MonoBehaviours
{
    public class TileMonoBehaviour : MonoBehaviour
    {
        public ITile Tile { get; private set; }

        private void Awake()
        {
            Tile = new Tile(gameObject);
            TileTracker.Instance.OnTileCreated(Tile);
        }
    }
}
