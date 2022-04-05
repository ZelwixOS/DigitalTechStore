namespace WebApi.Tests.ProductsControllerTests
{
    using System;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class ProductsControllerCreateTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public ProductsControllerCreateTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Create_ProductCreateRequestDto_ProductDto()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            ProductCreateRequestDto productArgument = testObjects.GetProductCreateRequestDtoNotebook(category.Id);
            ProductDto productExpected = testObjects.GetProductDtoNotebook();

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Create(productArgument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            ProductDto product = (ProductDto)okObjectresult.Value;
            Assert.True(product.CompareTo(productExpected) == 1);
        }

        [Fact]
        public void Create_ProductCreateRequestDto_Null_NoCategoryInDB()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            ProductCreateRequestDto productArgument = testObjects.GetProductCreateRequestDtoNotebook(Guid.NewGuid());

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Create(productArgument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            ProductDto product = (ProductDto)okObjectresult.Value;
            Assert.Null(product);
        }

        [Fact]
        public void Create_ProductCreateRequestDto_Null_MarkBigger5()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            ProductCreateRequestDto productArgument = testObjects.GetProductCreateRequestDtoNotebook(category.Id);
            productArgument.Mark = 9.8;

            var dataBaseInitializer = this.testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category);
                x.SaveChanges();
            });

            var controllerFactory = this.testInitFactory.CreateControllerFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Create(productArgument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            ProductDto product = (ProductDto)okObjectresult.Value;
            Assert.Null(product);
        }
    }
}
