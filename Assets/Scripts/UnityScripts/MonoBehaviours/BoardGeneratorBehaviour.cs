using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.UnityScripts.MonoBehaviours
{
    public class BoardGeneratorBehaviour : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 9)]
        private int _width = 3;

        [SerializeField]
        private GameObject _cell;

        private IBoardGenerator _boardGenerator;

        private void Awake()
        {
            _boardGenerator = BoardGenerator.Instance;
        }

        private void Start()
        {
            var spriteSize = _cell.GetComponent<SpriteRenderer>().bounds.size;
            _boardGenerator.CreateBoard(
                _width,
                spriteSize,
                transform,
                _cell);
        }
    }
}
