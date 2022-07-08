using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.MonoBehaviours
{
    public class BoardMonoBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int _width = 3;

        [SerializeField]
        private int _height = 3;

        [SerializeField]
        private GameObject _cell;

        private IBoardManager _boardManager;

        private void Awake()
        {
            _boardManager = BoardManager.Instance;
        }

        private void Start()
        {
            var spriteSize = _cell.GetComponent<SpriteRenderer>().bounds.size;
            _boardManager.CreateBoard(
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
