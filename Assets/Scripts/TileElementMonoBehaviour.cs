using System.Collections.Generic;
using UnityEngine;

namespace MergeMechanic
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

        private int _level;

        private void Awake()
        {
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            var newSprite = _shapes[0];
            _spriteRenderer.sprite = newSprite;
            _level = 1;
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

            if (_collided && _triggered._level == _level)
            {
                _triggered.IncrementLevel();
                _level = 1;
                BoardManager.Instance.OnMerge(this);
            }
        }

        private void IncrementLevel()
        {
            _spriteRenderer.sprite = _shapes[_level];
            _level++;
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (_collided)
            {
                return;
            }

            var tileElement = col.gameObject.GetComponent<TileElementMonoBehaviour>();

            if (tileElement)
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

        public void Show()
        {
            _spriteRenderer.sprite = _shapes[0];
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _spriteRenderer.sprite = null;
            gameObject.SetActive(false);
        }
    }
}
