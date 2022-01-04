namespace WebApi.Tests.CategoryControllerTests
{
    using System.Collections.Generic;
    using Application.ViewModels;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class CategoryControllerGetAllTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public CategoryControllerGetAllTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Get_NoArgs_ListCategoryDto()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product11 = testObjects.GetProductNotebook(category1.Id, category1);
            Category category2 = testObjects.GetCategoryPhones();
            Product product21 = testObjects.GetProductPhone(category2.Id, category2);

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Categories.Add(category2);
                x.Products.Add(product11);
                x.Products.Add(product21);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            List<CategoryDto> listOfCategories = (List<CategoryDto>)okObjectresult.Value;
            Assert.True(listOfCategories.Count == 2);
        }

        [Fact]
        public void Get_NoArgs_EmptyListCategoryDto_NoCategoriesInDB()
        {
            // Arrange
            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            List<CategoryDto> listOfCategories = (List<CategoryDto>)okObjectresult.Value;
            Assert.Empty(listOfCategories);
        }
    }
}
