using UnityEngine;

namespace MergeMechanic.Core
{
    public class GameObjectWrapper : IGameObjectWrapper
    {
        public GameObject Instantiate(
            GameObject gameObjectToInstantiate,
            Vector3 position,
            Transform parent,
            string name)
        {
            var gameObject = Object.Instantiate(gameObjectToInstantiate, position, gameObjectToInstantiate.transform.rotation);
            gameObject.transform.parent = parent;
            gameObject.name = name;
            return gameObject;
        }

        public GameObject Instantiate(GameObject gameObjectToInstantiate, Transform parent)
        {
            var gameObject = Object.Instantiate(gameObjectToInstantiate, parent.transform.position, Quaternion.identity);
            gameObject.transform.parent = parent;
            return gameObject;
        }

        public void Destroy(GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }

        public void ResetLocalPosition(GameObject gameObject)
        {
            gameObject.transform.localPosition = Vector3.zero;
        }
    }
}
