namespace WebApi.Tests.CategoryControllerTests
{
    using Application.DTO.Request;
    using Application.ViewModels;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class CategoryControllerUpdateTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public CategoryControllerUpdateTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Update_CategoryUpdateRequestDto_CategoryDto()
        {
            // Arrange
            Category categoryOriginal = testObjects.GetCategoryPhones();
            CategoryUpdateRequestDto categoryArgument = testObjects.GetCategoryUpdateRequestDtoNotebooks(categoryOriginal.Id);
            CategoryDto categoryExpected = testObjects.GetCategoryResponseNotebooks();
            categoryExpected.Id = categoryOriginal.Id;

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(categoryOriginal);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Update(categoryArgument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CategoryDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            CategoryDto category = (CategoryDto)okObjectresult.Value;
            Assert.True(category.CompareTo(categoryExpected) == 0);
        }
    }
}
