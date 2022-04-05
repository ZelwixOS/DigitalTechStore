namespace WebApi.Tests.ProductsControllerTests
{
    using System;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class ProductsControllerUpdateTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public ProductsControllerUpdateTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Update_ProductUpdateRequestDto_ProductDto()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            Product productOriginal = testObjects.GetProductNotebook(category.Id, category);
            var productArgument = testObjects.GetProductUpdateRequestDtoNotebook(productOriginal.Id, category.Id);

            var productExpected = new ProductDto()
            {
                Id = productOriginal.Id,
                Name = "Zelwix BookPad 22",
                Price = 444,
                Description = "This notebook doesn't exist right now...",
                Category = null,
                Mark = 4.9,
                PicURL = "22.jpg",
                VendorCode = "1234666",
            };

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
            var result = productController.Update(productArgument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            ProductDto product = (ProductDto)okObjectresult.Value;
            Assert.True(product.CompareTo(productExpected) == 0);
        }

        [Fact]
        public void Update_ProductUpdateRequestDto_ProductDto_NoCategoryInDB()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            Product productOriginal = testObjects.GetProductNotebook(category.Id, category);

            var productArgument = new ProductUpdateRequestDto()
            {
                Id = productOriginal.Id,
                Name = "Zelwix BookPad 22",
                Price = 444,
                Description = "This notebook doesn't exist right now...",
                CategoryId = Guid.NewGuid(),
                Mark = 4.9,
                PicURL = "22.jpg",
                VendorCode = "1234666",
            };

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
            var result = productController.Update(productArgument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            ProductDto product = (ProductDto)okObjectresult.Value;
            Assert.Null(product);
        }
    }
}
