using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.MonoBehaviours
{
    public class GenerateButtonBehaviour : MonoBehaviour
    {
        private IBoardManager _boardManager;

        [SerializeField]
        private GameObject _gameObjectToGenerate;

        private void Awake()
        {
            _boardManager = BoardManager.Instance;
        }

        public void OnButtonClick()
        {
            _boardManager.PopulateTile(_gameObjectToGenerate);
        }
    }
}
