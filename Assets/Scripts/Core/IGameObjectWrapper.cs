using UnityEngine;

namespace MergeMechanic.Core
{
    public interface IGameObjectWrapper
    {
        public GameObject Instantiate(GameObject gameObjectToInstantiate, Vector3 position, Transform parent);
    }
}
