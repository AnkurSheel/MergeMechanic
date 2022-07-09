using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.MonoBehaviours
{
    public class TileMonoBehaviour : MonoBehaviour
    {
        public ITile Tile { get; private set; }

        private void Awake()
        {
            Tile = new Tile(gameObject);
        }
    }
}
