using System.Collections.Generic;
using MergeMechanic.Core;
using UnityEngine;

namespace MergeMechanic.UnityScripts.MonoBehaviours
{
    public class TileElementBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> _shapes = new List<Sprite>();

        private SpriteRenderer _spriteRenderer;
        private bool _selected;
        private Camera _camera;
        private TileElementBehaviour _triggeredTileElementBehavior;
        private ITileElement _tileElement;

        private void Awake()
        {
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _shapes[0];
            
        }

        private void Start()
        {
            var parentTile = GetComponentInParent<TileMonoBehaviour>();
            _tileElement = new TileElement(
                gameObject,
                parentTile.Tile,
                new GameObjectWrapper(),
                TileTracker.Instance);
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

            _tileElement.ResetLocalPosition();

            if (_triggeredTileElementBehavior != null)
            {
                _tileElement.OnMerge(_triggeredTileElementBehavior._tileElement, level => _triggeredTileElementBehavior._spriteRenderer.sprite = _shapes[level]);
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (_triggeredTileElementBehavior != null)
            {
                return;
            }

            var tileElementMonoBehaviour = collider.gameObject.GetComponent<TileElementBehaviour>();

            if (tileElementMonoBehaviour != null)
            {
                _triggeredTileElementBehavior = tileElementMonoBehaviour;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _triggeredTileElementBehavior = null;
        }
    }
}
