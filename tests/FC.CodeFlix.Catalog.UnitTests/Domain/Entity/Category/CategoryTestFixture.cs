﻿using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity;
public class CategoryTestFixture : BaseFixture
{
    public CategoryTestFixture() : base() { }

    public string GetValidCategoryName()
    {
        var categoryName = "";

        while (categoryName.Length < 3)        
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10000)
            categoryDescription = categoryDescription[..10000];

        return categoryDescription;
    }

    public Category GetValidCategory()
        => new Category(
            GetValidCategoryName(),
            GetValidCategoryDescription()
        );

}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }
