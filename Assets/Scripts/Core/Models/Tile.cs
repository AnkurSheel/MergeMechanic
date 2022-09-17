using UnityEngine;

namespace MergeMechanic.Core.Models
{
    public class Tile
    {
        private readonly GameObject _gameObject;

        public Tile(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public Transform GetTransform()
            => _gameObject.transform;
    }
}
