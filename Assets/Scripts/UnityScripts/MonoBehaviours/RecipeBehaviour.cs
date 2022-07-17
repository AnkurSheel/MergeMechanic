using System.Collections.Generic;
using MergeMechanic.Core;
using MergeMechanic.UnityScripts.ScriptableObject;
using UnityEngine;

namespace MergeMechanic.UnityScripts.MonoBehaviours
{
    public class RecipeBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Recipe _recipe;
        
        private SpriteRenderer _spriteRenderer;
        private bool _selected;
        private Camera _camera;
        private RecipeBehaviour _triggeredRecipeBehavior;
        private ITileElement _tileElement;

        private void Awake()
        {
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _recipe.RecipeItems[0].Image;
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

            if (_triggeredRecipeBehavior != null)
            {
                _tileElement.OnMerge(_triggeredRecipeBehavior._tileElement, level => _triggeredRecipeBehavior._spriteRenderer.sprite = _recipe.RecipeItems[level].Image);
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (_triggeredRecipeBehavior != null)
            {
                return;
            }

            var tileElementMonoBehaviour = collider.gameObject.GetComponent<RecipeBehaviour>();

            if (tileElementMonoBehaviour != null)
            {
                _triggeredRecipeBehavior = tileElementMonoBehaviour;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _triggeredRecipeBehavior = null;
        }
    }
}
