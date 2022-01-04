namespace WebApi.Tests.ProductsControllerTests
{
    using System;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class ProductsControllerDeleteTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public ProductsControllerDeleteTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Delete_ProductId_1()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            Product productOriginal = testObjects.GetProductNotebook(category.Id, category);

            Guid argument = productOriginal.Id;
            int expected = 1;

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.Products.Add(productOriginal);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Delete(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            int answerValue = (int)okObjectresult.Value;
            Assert.Equal(answerValue, expected);
        }

        [Fact]
        public void Delete_ProductId_0_NoItemInDB()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            Product productOriginal = testObjects.GetProductNotebook(category.Id, category);

            Guid argument = Guid.NewGuid();
            int expected = 0;

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.Products.Add(productOriginal);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Delete(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            int answerValue = (int)okObjectresult.Value;
            Assert.Equal(answerValue, expected);
        }
    }
}
