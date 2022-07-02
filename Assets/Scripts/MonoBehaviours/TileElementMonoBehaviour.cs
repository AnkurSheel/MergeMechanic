using System.Collections.Generic;
using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.MonoBehaviours
{
    public class TileElementMonoBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> _shapes = new List<Sprite>();

        private SpriteRenderer _spriteRenderer;

        private bool _selected;
        private Camera _camera;

        private TileElementMonoBehaviour _triggeredTileElementMonoBehavior;

        public ITileElement TileElement { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _shapes[0];
            TileElement = new TileElement(gameObject, new GameObjectWrapper());
        }

        private void OnEnable()
        {
            _spriteRenderer.sprite = _shapes[0];
        }

        private void OnDisable()
        {
            _spriteRenderer.sprite = null;
        }

        private void Update()
        {
            if (_selected)
            {
                var screenToWorldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
            }
        }

        private void OnMouseDown()
        {
            _selected = true;
        }

        private void OnMouseUp()
        {
            _selected = false;

            TileElement.ResetLocalPosition();

            if (_triggeredTileElementMonoBehavior != null)
            {
                TileElement.OnMerge(_triggeredTileElementMonoBehavior.TileElement, level => _triggeredTileElementMonoBehavior._spriteRenderer.sprite = _shapes[level]);
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (_triggeredTileElementMonoBehavior != null)
            {
                return;
            }

            var tileElementMonoBehaviour = collider.gameObject.GetComponent<TileElementMonoBehaviour>();

            if (tileElementMonoBehaviour != null)
            {
                _triggeredTileElementMonoBehavior = tileElementMonoBehaviour;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _triggeredTileElementMonoBehavior = null;
        }
    }
}
