using MergeMechanic.Core;
using MergeMechanic.Core.Models;
using UnityEngine;

namespace MergeMechanic.UnityScripts.MonoBehaviours
{
    public class TileMB : MonoBehaviour
    {
        public Tile Tile { get; private set; }

        private void Awake()
        {
            Tile = new Tile(gameObject);
            var tileTracker = DependencyHelper.GetRequiredService<ITileTracker>();
            tileTracker.AddEmptyTile(Tile);
        }
    }
}
