namespace WebApi.Tests.CategoryControllerTests
{
    using Application.DTO.Request;
    using Application.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class CategoryControllerCreateTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public CategoryControllerCreateTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Create_CategoryCreateRequestDto_CategoryDto()
        {
            // Arrange
            CategoryCreateRequestDto argument = testObjects.GetCategoryCreateRequestArgumentNotebooks();
            CategoryDto expected = testObjects.GetCategoryResponseNotebooks();

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Create(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CategoryDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            CategoryDto category = (CategoryDto)okObjectresult.Value;
            Assert.True(category.CompareTo(expected) == 1);
        }
    }
}
