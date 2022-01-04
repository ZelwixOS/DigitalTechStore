namespace WebApi.Tests.CategoryControllerTests
{
    using System;
    using System.Collections.Generic;
    using Application.ViewModels;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.Mock;
    using Xunit;

    public class CategoryControllerGetById
    {
        private readonly TestInitFactory testInitFactory = new TestInitFactory();

        private readonly TestObjectsFactory testObjects;

        public CategoryControllerGetById()
        {
            this.testObjects = new TestObjectsFactory();
        }

        [Fact]
        public void Get_Guid_CategoryDto()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product11 = testObjects.GetProductNotebook(category1.Id, category1);

            Category category2 = testObjects.GetCategoryPhones();
            Product product21 = testObjects.GetProductPhone(category2.Id, category2);

            var expected = new CategoryDto(category1)
            {
                Products = new HashSet<ProductOfCategoryDto>(),
            };
            expected.Products.Add(new ProductOfCategoryDto(product11));

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
            var result = categoryController.Get(category1.Id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CategoryDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            CategoryDto category = (CategoryDto)okObjectresult.Value;
            Assert.True(category.CompareTo(expected) == 0 && category.Products.Count == expected.Products.Count);
        }

        [Fact]
        public void Get_Guid_NullCategoryDto_NoCategoryInDB()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            Product product11 = testObjects.GetProductNotebook(category1.Id, category1);
            var argument = Guid.NewGuid();

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
            var result = categoryController.Get(argument);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CategoryDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            CategoryDto category = (CategoryDto)okObjectresult.Value;
            Assert.Null(category);
        }

        [Fact]
        public void Get_Guid_CategoryDto_NoProducts()
        {
            // Arrange
            Category category1 = testObjects.GetCategoryNotebooks();
            category1.Products = new HashSet<Product>();
            var expected = new CategoryDto(category1);

            var dataBaseInitializer = testInitFactory.CreateDbInitalizer();
            dataBaseInitializer.InitDataBase(x =>
            {
                x.Categories.Add(category1);
                x.SaveChanges();
            });

            var controllerFactory = testInitFactory.CreateControllerFactory();
            var categoryController = controllerFactory.CreateCategoryController();

            // Act
            var result = categoryController.Get(category1.Id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CategoryDto>>(result);
            var okObjectresult = (OkObjectResult)actionResult.Result;
            CategoryDto category = (CategoryDto)okObjectresult.Value;
            Assert.True(category.CompareTo(expected) == 0 && category.Products.Count == expected.Products.Count);
        }
    }
}
