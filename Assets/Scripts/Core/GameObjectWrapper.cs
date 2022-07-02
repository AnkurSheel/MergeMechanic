﻿using UnityEngine;

namespace MergeMechanic.Core
{
    public class GameObjectWrapper : IGameObjectWrapper
    {
        public GameObject Instantiate(GameObject gameObjectToInstantiate, Vector3 position, Transform parent)
        {
            var gameObject = Object.Instantiate(gameObjectToInstantiate, position, gameObjectToInstantiate.transform.rotation);
            gameObject.transform.parent = parent;
            return gameObject;
        }

        public void SetActive(GameObject gameObject, bool value)
        {
            gameObject.SetActive(value);
        }

        public void ResetLocalPosition(GameObject gameObject)
        {
            gameObject.transform.localPosition = Vector3.zero;
        }
    }
}
