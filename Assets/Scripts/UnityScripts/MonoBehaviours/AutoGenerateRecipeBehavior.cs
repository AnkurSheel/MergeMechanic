using UnityEngine;

namespace MergeMechanic.UnityScripts.MonoBehaviours
{
    public class AutoGenerateRecipeBehavior : MonoBehaviour
    {
        [SerializeField]
        private float _repeatRate = 2.0f;

        [SerializeField]
        private RecipeBehaviour _recipe;
        
        private void Start()
        {
            
            InvokeRepeating(nameof(CreateRecipe), 0.0f, _repeatRate);
        }

        private void CreateRecipe()
        {
            _recipe.CreateRecipe(1);
        }
    }
}
