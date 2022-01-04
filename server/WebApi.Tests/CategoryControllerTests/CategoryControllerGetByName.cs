namespace WebApi.Tests.CategoryControllerTests
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response.WithExtraInfo;
    using Application.ViewModels;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class CategoryControllerGetByName
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public CategoryControllerGetByName()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product11 = testObjects.GetProductNotebook(category1.Id, category1);
            Category category2 = testObjects.GetCategoryPhones();
            Product product21 = testObjects.GetProductPhone(category2.Id, category2);
            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9 };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 1, 333, 333);
            expected.Container.Products = new HashSet<ProductOfCategoryDto>();
            expected.Container.Products.Add(new ProductOfCategoryDto(product11));

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
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            var products = new List<ProductOfCategoryDto>(categoryWithInfo.Container.Products);
            Assert.True(products[0].CompareTo(new ProductOfCategoryDto(product11)) == 0);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_NullCategoryDto_NoCategoryInDB()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product11 = testObjects.GetProductNotebook(category1.Id, category1);
            string argument1 = "Phones";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9 };

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product11);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> category = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.Null(category);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_NoProducts()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 0, 0, 0);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9 };

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> category = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(category.Container.CompareTo(expected.Container) == 0);
            Assert.Empty(category.Container.Products);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_PageNumber2ItemsOnPage1()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category1.Id, category1);
            Product product2 = testObjects.GetProductNotebook2(category1.Id, category1);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 2, ItemsOnPage = 1 };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 2, 322, 333);
            expected.Container.Products = new HashSet<ProductOfCategoryDto>();
            expected.Container.Products.Add(new ProductOfCategoryDto(product1));

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            var products = new List<ProductOfCategoryDto>(categoryWithInfo.Container.Products);
            Assert.True(products[0].CompareTo(new ProductOfCategoryDto(product1)) == 0);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_NumerOfItemsOnPageIncorrect()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product11 = testObjects.GetProductNotebook(category1.Id, category1);

            Category category2 = testObjects.GetCategoryPhones();
            Product product21 = testObjects.GetProductPhone(category2.Id, category2);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 0 };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 0, 333, 333);

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
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            Assert.Empty(categoryWithInfo.Container.Products);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_PageNumerIncorrect()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product11 = testObjects.GetProductNotebook(category1.Id, category1);

            Category category2 = testObjects.GetCategoryPhones();
            Product product21 = testObjects.GetProductPhone(category2.Id, category2);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 0, ItemsOnPage = 9 };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 1, 333, 333);

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
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            Assert.Empty(categoryWithInfo.Container.Products);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_MaxPrice330()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category1.Id, category1);
            Product product2 = testObjects.GetProductNotebook2(category1.Id, category1);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9, MaxPrice = 330 };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 1, 322, 322);
            expected.Container.Products = new HashSet<ProductOfCategoryDto>();
            expected.Container.Products.Add(new ProductOfCategoryDto(product2));

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            var products = new List<ProductOfCategoryDto>(categoryWithInfo.Container.Products);
            Assert.True(products[0].CompareTo(new ProductOfCategoryDto(product2)) == 0);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_MaxinPrice330()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category1.Id, category1);
            Product product2 = testObjects.GetProductNotebook2(category1.Id, category1);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9, MinPrice = 330 };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 1, 333, 333);
            expected.Container.Products = new HashSet<ProductOfCategoryDto>();
            expected.Container.Products.Add(new ProductOfCategoryDto(product1));

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            var products = new List<ProductOfCategoryDto>(categoryWithInfo.Container.Products);
            Assert.True(products[0].CompareTo(new ProductOfCategoryDto(product1)) == 0);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_MinPriceGreaterThanMaxPrice()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category1.Id, category1);
            Product product2 = testObjects.GetProductNotebook2(category1.Id, category1);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9, MinPrice = 400, MaxPrice = 200 };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 0, 0, 0);

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            Assert.Empty(categoryWithInfo.Container.Products);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_MinPriceSameAsMaxPrice()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category1.Id, category1);
            Product product2 = testObjects.GetProductNotebook2(category1.Id, category1);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9, MinPrice = 333, MaxPrice = 333 };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 1, 333, 333);
            expected.Container.Products = new HashSet<ProductOfCategoryDto>();
            expected.Container.Products.Add(new ProductOfCategoryDto(product1));

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            var products = new List<ProductOfCategoryDto>(categoryWithInfo.Container.Products);
            Assert.True(products[0].CompareTo(new ProductOfCategoryDto(product1)) == 0);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_SortedByPrice()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category1.Id, category1);
            Product product2 = testObjects.GetProductNotebook2(category1.Id, category1);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9, SortingType = SortingType.Price };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 1, 322, 333);
            expected.Container.Products = new HashSet<ProductOfCategoryDto>();
            expected.Container.Products.Add(new ProductOfCategoryDto(product2));
            expected.Container.Products.Add(new ProductOfCategoryDto(product2));

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            var products = new List<ProductOfCategoryDto>(categoryWithInfo.Container.Products);
            Assert.True(products[0].CompareTo(new ProductOfCategoryDto(product2)) == 0);
            Assert.True(products[1].CompareTo(new ProductOfCategoryDto(product1)) == 0);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_SortedByName()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category1.Id, category1);
            Product product2 = testObjects.GetProductNotebook2(category1.Id, category1);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9, SortingType = SortingType.Name };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 1, 322, 333);
            expected.Container.Products = new HashSet<ProductOfCategoryDto>();
            expected.Container.Products.Add(new ProductOfCategoryDto(product2));
            expected.Container.Products.Add(new ProductOfCategoryDto(product2));

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            var products = new List<ProductOfCategoryDto>(categoryWithInfo.Container.Products);
            Assert.True(products[0].CompareTo(new ProductOfCategoryDto(product2)) == 0);
            Assert.True(products[1].CompareTo(new ProductOfCategoryDto(product1)) == 0);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }

        [Fact]
        public void Get_NameGetProductsRequest_CategoryDto_SortedByNameReverse()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product1 = testObjects.GetProductNotebook(category1.Id, category1);
            Product product2 = testObjects.GetProductNotebook2(category1.Id, category1);

            string argument1 = "Notebooks";
            var argument2 = new GetProductsRequest() { PageNumber = 1, ItemsOnPage = 9, SortingType = SortingType.Name, ReverseSorting = true };

            var expected = new WrapperExtraInfo<CategoryDto>(new CategoryDto(category1), 1, 322, 333);
            expected.Container.Products = new HashSet<ProductOfCategoryDto>();
            expected.Container.Products.Add(new ProductOfCategoryDto(product2));
            expected.Container.Products.Add(new ProductOfCategoryDto(product2));

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.Products.Add(product1);
                x.Products.Add(product2);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(argument1, argument2);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WrapperExtraInfo<CategoryDto>>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            WrapperExtraInfo<CategoryDto> categoryWithInfo = (WrapperExtraInfo<CategoryDto>)okObjectresult.Value;
            Assert.True(categoryWithInfo.Container.CompareTo(expected.Container) == 0);
            var products = new List<ProductOfCategoryDto>(categoryWithInfo.Container.Products);
            Assert.True(products[0].CompareTo(new ProductOfCategoryDto(product1)) == 0);
            Assert.True(products[1].CompareTo(new ProductOfCategoryDto(product2)) == 0);
            Assert.True(categoryWithInfo.MaxPage == expected.MaxPage && categoryWithInfo.MinPrice == expected.MinPrice && categoryWithInfo.MaxPrice == expected.MaxPrice);
        }
    }
}
