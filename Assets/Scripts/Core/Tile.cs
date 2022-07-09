using UnityEngine;

namespace MergeMechanic.Core
{
    public class Tile : ITile
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
