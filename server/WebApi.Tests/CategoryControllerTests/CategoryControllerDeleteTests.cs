namespace WebApi.Tests.CategoryControllerTests
{
    using System;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class CategoryControllerDeleteTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public CategoryControllerDeleteTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Delete_CategoryId_1()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            Guid argument = category.Id;
            int expected = 1;

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Delete(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            int answerValue = (int)okObjectresult.Value;
            Assert.Equal(answerValue, expected);
        }

        [Fact]
        public void Delete_CategoryId_0_NoCategoryInDB()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            Guid argument = Guid.NewGuid();
            int expected = 0;

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Delete(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            int answerValue = (int)okObjectresult.Value;
            Assert.Equal(answerValue, expected);
        }

        [Fact]
        public void Delete_CategoryId_0_CategoryHasProduct()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            Product product = testObjects.GetProductNotebook(category.Id, category);

            Guid argument = category.Id;

            int expected = 0;

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.Products.Add(product);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Delete(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            int answerValue = (int)okObjectresult.Value;
            Assert.Equal(answerValue, expected);
        }
    }
}
