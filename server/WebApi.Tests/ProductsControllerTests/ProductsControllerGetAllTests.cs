namespace WebApi.Tests.ProductsControllerTests
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response.WithExtraInfo;
    using Application.ViewModels;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class ProductsControllerGetAllTests
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public ProductsControllerGetAllTests()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo()
        {
            // Arrange
            DBInit();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1 };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;
            Assert.True(productsWInfo.Container.Count == 2 && productsWInfo.MaxPage == 1 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 333);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_NoProductsInDB()
        {
            // Arrange
            Category category = testObjects.GetCategoryNotebooks();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1 };

            var dataBaseInitializer = testInitFactory.CreateDbInitalizerInMemory();
            dataBaseInitializer.InitDataBaseInMemory(x =>
            {
                x.Categories.Add(category);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;
            Assert.True(productsWInfo.MinPrice == 0 && productsWInfo.MaxPrice == 0 && productsWInfo.MaxPage == 0);
            Assert.Empty(productsWInfo.Container);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_PageNumber2NumberOfItems1()
        {
            // Arrange
            var tuple = DBInit();

            var argument = new GetProductsRequest() { ItemsOnPage = 1, PageNumber = 2 };
            var expected = new ProductDto(tuple.Item2);
            expected.Category = null;

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;
            Assert.True(productsWInfo.Container.Count == 1 && productsWInfo.MaxPage == 2 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 333);
            Assert.True(productsWInfo.Container[0].CompareTo(expected) == 0);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_NumerOfItemsOnPageIncorrect()
        {
            // Arrange
            DBInit();

            var argument = new GetProductsRequest() { ItemsOnPage = 0, PageNumber = 1 };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;
            Assert.Empty(productsWInfo.Container);
            Assert.True(productsWInfo.MaxPage == 0 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 333);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_PageNumerIncorrect()
        {
            // Arrange
            DBInit();

            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 0 };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;
            Assert.Empty(productsWInfo.Container);
            Assert.True(productsWInfo.MaxPage == 1 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 333);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_MaxPrice330()
        {
            // Arrange
            DBInit();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1, MaxPrice = 330 };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;

            Assert.True(productsWInfo.Container.Count == 1 && productsWInfo.MaxPage == 1 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 322);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_MinPrice330()
        {
            // Arrange
            DBInit();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1, MinPrice = 330 };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;

            Assert.True(productsWInfo.Container.Count == 1 && productsWInfo.MaxPage == 1 && productsWInfo.MinPrice == 333 && productsWInfo.MaxPrice == 333);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_MinPriceGreaterThanMaxPrice()
        {
            // Arrange
            DBInit();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1, MinPrice = 350, MaxPrice = 310 };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;

            Assert.Empty(productsWInfo.Container);
            Assert.True(productsWInfo.MaxPage == 0 && productsWInfo.MinPrice == 0 && productsWInfo.MaxPrice == 0);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_MinPriceSameAsMaxPrice()
        {
            // Arrange
            DBInit();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1, MinPrice = 322, MaxPrice = 322 };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;

            Assert.True(productsWInfo.Container.Count == 1 && productsWInfo.MaxPage == 1 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 322);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_SortedByPrice()
        {
            // Arrange
            var tuple = DBInit();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1, SortingType = SortingType.Price };

            var expected1 = new ProductDto(tuple.Item3)
            {
                Category = null,
            };
            var expected2 = new ProductDto(tuple.Item2)
            {
                Category = null,
            };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;

            Assert.True(productsWInfo.Container.Count == 2 && productsWInfo.MaxPage == 1 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 333);
            Assert.True(productsWInfo.Container[0].CompareTo(expected1) == 0);
            Assert.True(productsWInfo.Container[1].CompareTo(expected2) == 0);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_SortedByName()
        {
            // Arrange
            var tuple = DBInit();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1, SortingType = SortingType.Name };

            var expected1 = new ProductDto(tuple.Item3)
            {
                Category = null,
            };
            var expected2 = new ProductDto(tuple.Item2)
            {
                Category = null,
            };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;

            Assert.True(productsWInfo.Container.Count == 2 && productsWInfo.MaxPage == 1 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 333);
            Assert.True(productsWInfo.Container[0].CompareTo(expected1) == 0);
            Assert.True(productsWInfo.Container[1].CompareTo(expected2) == 0);
        }

        [Fact]
        public void Get_GetProductsRequest_WrapperExtraInfo_SortedByNameReverse()
        {
            // Arrange
            var tuple = DBInit();
            var argument = new GetProductsRequest() { ItemsOnPage = 9, PageNumber = 1, SortingType = SortingType.Name, ReverseSorting = true };

            var expected1 = new ProductDto(tuple.Item2)
            {
                Category = null,
            };
            var expected2 = new ProductDto(tuple.Item3)
            {
                Category = null,
            };

            var controllerFactory = testInitFactory.CreateControllerInMemoryFactory();
            var productController = controllerFactory.CreateProductController();

            // Act
            var result = productController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<List<ProductDto>>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<List<ProductDto>> productsWInfo = (WrapperExtraInfo<List<ProductDto>>)okObjectresult.Value;

            Assert.True(productsWInfo.Container.Count == 2 && productsWInfo.MaxPage == 1 && productsWInfo.MinPrice == 322 && productsWInfo.MaxPrice == 333);
            Assert.True(productsWInfo.Container[0].CompareTo(expected1) == 0);
            Assert.True(productsWInfo.Container[1].CompareTo(expected2) == 0);
        }

        private (Category, Product, Product) DBInit()
        {
            Category category = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category.Id, category);
            Product product2 = testObjects.GetProductNotebook2(category.Id, category);

            var dataBaseInitializer = testInitFactory.CreateDbInitalizerInMemory();
            dataBaseInitializer.InitDataBaseInMemory(x =>
            {
                x.Categories.Add(category);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            return (category, product1, product2);
        }
    }
}
