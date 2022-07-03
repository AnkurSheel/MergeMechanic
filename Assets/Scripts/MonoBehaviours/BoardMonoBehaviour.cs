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

        [SerializeField]
        private float _repeatRate = 2.0f;

        private IBoardManager _boardManager;

        private void Awake()
        {
            _boardManager = new BoardManager(new GridHelper(), new GameObjectWrapper(), TileTracker.Instance);
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
                GetTileElement);

            InvokeRepeating(nameof(PopulateTile), 0.0f, _repeatRate);
        }

        private ITileElement GetTileElement(GameObject tile)
        {
            var tileElement = tile.GetComponentInChildren<TileElementMonoBehaviour>();
            return tileElement.TileElement;
        }

        public void PopulateTile()
        {
            _boardManager.PopulateTile();
        }
    }
}
