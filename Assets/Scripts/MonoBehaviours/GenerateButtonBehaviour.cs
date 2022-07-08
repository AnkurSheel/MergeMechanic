using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.MonoBehaviours
{
    public class GenerateButtonBehaviour : MonoBehaviour
    {
        private IBoardGenerator _boardGenerator;

        [SerializeField]
        private GameObject _gameObjectToGenerate;

        private void Awake()
        {
            _boardGenerator = BoardGenerator.Instance;
        }

        public void OnButtonClick(int amount)
        {
            _boardGenerator.PopulateTile(_gameObjectToGenerate, amount);
        }
    }
}
