using UnityEngine;

namespace MergeMechanic.UnityScripts.ScriptableObject
{
    [CreateAssetMenu(fileName = "RecipeItem", menuName = "Recipe Item", order = 0)]
    public class RecipeItem : UnityEngine.ScriptableObject
    {
        public Sprite Image;
    }
}
