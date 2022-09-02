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
        private readonly IBoardGenerator _boardGenerator;
        private readonly ITileTracker _tileTracker;

        public RecipeBehaviour()
        {
            _boardGenerator = DependencyHelper.GetRequiredService<IBoardGenerator>();
            _tileTracker = DependencyHelper.GetRequiredService<ITileTracker>();
        }

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
                parentTile.Tile,
                new GameObjectWrapper(),
                _tileTracker);
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

            _tileElement.ResetLocalPosition(gameObject);

            if (_triggeredRecipeBehavior != null)
            {
                _tileElement.OnMerge(
                    _triggeredRecipeBehavior._tileElement,
                    gameObject,
                    level =>
                    {
                        if (level < _recipe.RecipeItems.Count)
                        {
                            _triggeredRecipeBehavior._spriteRenderer.sprite = _recipe.RecipeItems[level].Image;
                            return true;
                        }

                        return false;
                    });
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

        public void CreateRecipe(int amount)
        {
            _boardGenerator.PopulateTile(gameObject, amount);
        }
    }
}
