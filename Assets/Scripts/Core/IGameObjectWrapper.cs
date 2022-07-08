using UnityEngine;

namespace MergeMechanic.Core
{
    public interface IGameObjectWrapper
    {
        public GameObject Instantiate(GameObject gameObjectToInstantiate, Transform parent);

        public GameObject Instantiate(
            GameObject gameObjectToInstantiate,
            Vector3 position,
            Transform parent,
            string name);

        void Destroy(GameObject gameObject);

        void ResetLocalPosition(GameObject gameObject);
    }
}
