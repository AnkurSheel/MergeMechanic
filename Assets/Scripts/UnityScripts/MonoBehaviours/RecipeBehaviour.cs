using MergeMechanic.Core;
using MergeMechanic.Core.Models;
using MergeMechanic.Core.Recipe;
using MergeMechanic.UnityScripts.ScriptableObject;
using UnityEngine;

namespace MergeMechanic.UnityScripts.MonoBehaviours
{
    public class RecipeBehaviour : MonoBehaviour
    {
        [SerializeField]
        private RecipeSO _recipeSO;

        private SpriteRenderer _spriteRenderer;
        private bool _selected;
        private Camera _camera;
        private RecipeBehaviour _triggeredRecipeBehavior;
        private IRecipe _recipe;
        private readonly ITileTracker _tileTracker;
        private readonly IRecipeMerger _recipeMerger;
        private Tile _tile;

        public RecipeBehaviour()
        {
            _tileTracker = DependencyHelper.GetRequiredService<ITileTracker>();
            _recipeMerger = DependencyHelper.GetRequiredService<IRecipeMerger>();
        }

        private void Awake()
        {
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _recipeSO.RecipeItems[0].Image;
        }

        private void Start()
        {
            var parentTile = GetComponentInParent<TileMonoBehaviour>();
            _tile = parentTile.Tile;
            _recipe = new Recipe(
                _recipeSO.GetInstanceID(),
                _recipeSO.RecipeItems.Count,
                gameObject,
                DependencyHelper.GetRequiredService<IGameObjectWrapper>());
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

            _recipe.ResetLocalPosition();

            if (_triggeredRecipeBehavior != null)
            {
                var triggeredRecipeBehavior = _triggeredRecipeBehavior;
                var triggeredRecipe = _triggeredRecipeBehavior._recipe;
                var recipeItems = _recipeSO.RecipeItems;

                var isMerged = _recipeMerger.OnMerge(_recipe, triggeredRecipe, _tile);

                if (isMerged)
                {
                    triggeredRecipeBehavior._spriteRenderer.sprite = recipeItems[triggeredRecipe.CurrentLevel - 1].Image;
                }
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
            _tileTracker.PopulateTile(gameObject, amount);
        }
    }
}
