namespace WebApi.Tests.ProductsControllerTests
{
    using System;
    using Application.DTO.Response;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class ProductsControllerGetByIdTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public ProductsControllerGetByIdTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Get_Guid_ProductDto()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category.Id, category);
            Product product2 = testObjects.GetProductNotebook2(category.Id, category);

            CategoryOfProductDto categoryOfExpectedProduct = testObjects.GetCategoryOfProductNotebooks();
            ProductDto productExpected = testObjects.GetProductDtoNotebook(categoryOfExpectedProduct);
            productExpected.Id = product1.Id;

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(product1.Id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            ProductDto product = (ProductDto)okObjectresult.Value;
            Assert.True(product.CompareTo(productExpected) == 0);
        }

        [Fact]
        public void Get_Guid_Null_NoProductWithIdInDB()
        {
            // Arrange
            var argument = Guid.NewGuid();
            Category category = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category.Id, category);
            Product product2 = testObjects.GetProductNotebook2(category.Id, category);

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            ProductDto product = (ProductDto)okObjectresult.Value;
            Assert.Null(product);
        }
    }
}
