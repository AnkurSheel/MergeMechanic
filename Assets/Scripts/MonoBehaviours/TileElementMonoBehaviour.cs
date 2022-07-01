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
        private bool _collided;
        private Camera _camera;
        
        private TileElementMonoBehaviour _triggered;

        public ITileElement TileElement { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            var newSprite = _shapes[0];
            _spriteRenderer.sprite = newSprite;
            TileElement = new TileElement(gameObject);
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

            gameObject.transform.localPosition = Vector3.zero;

            if (_collided)
            {
                TileElement.OnMerge(_triggered.TileElement, level => _triggered._spriteRenderer.sprite = _shapes[level]);
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (_collided)
            {
                return;
            }

            var tileElement = collider.gameObject.GetComponent<TileElementMonoBehaviour>();

            if (tileElement != null)
            {
                _triggered = tileElement;
                _collided = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _triggered = null;
            _collided = false;
        }
    }
}
