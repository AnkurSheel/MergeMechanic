using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.MonoBehaviours
{
    public class BoardGeneratorBehaviour : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 9)]
        private int _width = 3;

        [SerializeField]
        [Range(0, 9)]
        private int _height = 3;

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
                _height,
                spriteSize,
                transform,
                _cell,
                GetTile);
        }

        private ITile GetTile(GameObject tile)
        {
            var tileElement = tile.GetComponent<TileMonoBehaviour>();
            return tileElement.Tile;
        }
    }
}
