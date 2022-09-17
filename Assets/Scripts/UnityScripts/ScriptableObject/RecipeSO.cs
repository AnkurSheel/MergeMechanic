using System.Collections.Generic;
using UnityEngine;

namespace MergeMechanic.UnityScripts.ScriptableObject
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Recipe", order = 0)]
    public class RecipeSO : UnityEngine.ScriptableObject
    {
        public List<RecipeItemSO> RecipeItems;

    }
}
