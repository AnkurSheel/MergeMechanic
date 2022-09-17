using MergeMechanic.Core;
using MergeMechanic.Core.Models;
using MergeMechanic.Core.Recipe;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace MergeMechanic.Tests.Core
{
    [TestFixture]
    public class RecipeMergerTests
    {
        private IRecipeMerger _recipeMerger;
        private Mock<ITileTracker> _tileTracker;
        private Mock<IRecipe> _selectedRecipe;
        private Mock<IRecipe> _triggeredRecipe;
        private Tile _selectedTile;

        [SetUp]
        public void Setup()
        {
            _tileTracker = new Mock<ITileTracker>();
            _selectedRecipe = new Mock<IRecipe>();
            _triggeredRecipe = new Mock<IRecipe>();

            _selectedTile = new Tile(new GameObject());

            _recipeMerger = new RecipeMerger(_tileTracker.Object);

            _triggeredRecipe.Setup(x => x.CanMerge(_selectedRecipe.Object)).Returns(true);
            _triggeredRecipe.Setup(x => x.CanIncrementLevel()).Returns(true);
        }

        [Test]
        public void Does_not_merge_tiles_if_cannot_merge_the_two_recipes()
        {
            _triggeredRecipe.Setup(x => x.CanMerge(_selectedRecipe.Object)).Returns(false);

            var result = OnMerge();

            Assert.False(result);
            VerifyCallsAreNotMade();
        }

        [Test]
        public void Does_not_merge_tiles_if_cannot_increment_the_level_of_the_triggered_recipe()
        {
            _triggeredRecipe.Setup(x => x.CanIncrementLevel()).Returns(false);

            var result = OnMerge();

            Assert.False(result);
            VerifyCallsAreNotMade();
        }

        [Test]
        public void Merging_two_recipes_increments_the_level_of_the_triggered_recipe()
        {
            var result = OnMerge();

            Assert.True(result);

            _triggeredRecipe.Verify(x => x.IncrementLevel(), Times.Once);
        }

        [Test]
        public void Merging_two_recipes_removes_the_selected_recipe()
        {
            var result = OnMerge();

            Assert.True(result);

            _selectedRecipe.Verify(x => x.Destroy(), Times.Once);
        }

        [Test]
        public void Merging_two_recipes_marks_the_tile_as_empty()
        {
            var result = OnMerge();

            Assert.True(result);

            _tileTracker.Verify(x => x.MakeTileEmpty(_selectedTile), Times.Once);
        }

        private bool OnMerge()
            => _recipeMerger.OnMerge(_selectedRecipe.Object, _triggeredRecipe.Object, _selectedTile);

        private void VerifyCallsAreNotMade()
        {
            _triggeredRecipe.Verify(x => x.IncrementLevel(), Times.Never);
            _selectedRecipe.Verify(x => x.Destroy(), Times.Never);
            _tileTracker.Verify(x => x.MakeTileEmpty(_selectedTile), Times.Never);
        }
    }
}
