using MergeMechanic.Core;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace MergeMechanic.Tests
{
    [TestFixture]
    public class RecipeTests
    {
        private Mock<IGameObjectWrapper> _gameObjectWrapper;
        private IRecipe _recipe;
        private GameObject _gameObject;
        private const int MaxNumberOfLevels = 3;

        [SetUp]
        public void Setup()
        {
            _gameObjectWrapper = new Mock<IGameObjectWrapper>();
            _gameObject = new GameObject();
            _recipe = GetRecipe();
        }

        [Test]
        public void Level_is_reset_on_instantiation()
        {
            Assert.AreEqual(1, _recipe.CurrentLevel);
        }

        [Test]
        public void Level_is_incremented_correctly_if_it_is_less_than_maximum_number_of_levels()
        {
            for (var i = 0; i < MaxNumberOfLevels; i++)
            {
                _recipe.IncrementLevel();
            }

            Assert.AreEqual(MaxNumberOfLevels, _recipe.CurrentLevel);
        }

        [Test]
        public void Level_is_not_incremented_if_cannot_merge()
        {
            const int numberOfIncrements = MaxNumberOfLevels + 1;

            for (var i = 0; i < numberOfIncrements; i++)
            {
                _recipe.IncrementLevel();
            }

            Assert.AreEqual(MaxNumberOfLevels, _recipe.CurrentLevel);
        }

        [Test]
        public void Reset_Local_Position_resets_the_position_of_the_gameobject()
        {
            _recipe.ResetLocalPosition();

            _gameObjectWrapper.Verify(x => x.ResetLocalPosition(_gameObject), Times.Once);
        }

        [Test]
        public void Can_increment_a_level_if_current_level_is_less_than_maximum_number_of_levels()
        {
            _recipe = GetRecipe(2);
            var result = _recipe.CanIncrementLevel();

            Assert.True(result);
        }

        [Test]
        public void Cannot_increment_a_level_if_current_level_is_same_as_maximum_number_of_levels()
        {
            _recipe = GetRecipe(1);
            var result = _recipe.CanIncrementLevel();

            Assert.False(result);
        }

        [Test]
        public void Can_merge_tiles()
        {
            var otherRecipe = GetRecipe();

            var result = _recipe.CanMerge(otherRecipe);

            Assert.True(result);
        }

        [Test]
        public void Cannot_merge_tiles_if_level_does_not_match()
        {
            var otherRecipe = GetRecipe(currentLevel: 2);
            var result = _recipe.CanMerge(otherRecipe);

            Assert.False(result);
        }

        [Test]
        public void Cannot_merge_tiles_if_type_does_not_match()
        {
            var otherRecipe = GetRecipe(type: 200);
            var result = _recipe.CanMerge(otherRecipe);

            Assert.False(result);
        }

        [Test]
        public void Destroying_a_recipe_resets_the_level()
        {
            _recipe.Destroy();

            Assert.AreEqual(1, _recipe.CurrentLevel);
        }

        [Test]
        public void Destroying_a_recipe_destroys_the_gameobject()
        {
            _recipe.Destroy();

            _gameObjectWrapper.Verify(x => x.Destroy(_gameObject), Times.Once);
        }

        private Recipe GetRecipe(int maxNumberOfLevels = MaxNumberOfLevels, int currentLevel = 1, int type = 100)
        {
            var recipe = new Recipe(
                type,
                maxNumberOfLevels,
                _gameObject,
                _gameObjectWrapper.Object);

            for (var i = 1; i < currentLevel; i++)
            {
                recipe.IncrementLevel();
            }

            return recipe;
        }
    }
}
