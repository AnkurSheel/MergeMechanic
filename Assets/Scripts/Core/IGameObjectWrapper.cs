using UnityEngine;

namespace MergeMechanic.Core
{
    public interface IGameObjectWrapper
    {
        public GameObject Instantiate(
            GameObject gameObjectToInstantiate,
            Vector3 position,
            Transform parent,
            string name);

        void SetActive(GameObject gameObject, bool value);

        void ResetLocalPosition(GameObject gameObject);
    }
}
