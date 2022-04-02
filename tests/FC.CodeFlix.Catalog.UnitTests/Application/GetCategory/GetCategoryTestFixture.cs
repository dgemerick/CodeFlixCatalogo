﻿
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection: ICollectionFixture<GetCategoryTestFixture>
{

}

public class GetCategoryTestFixture : BaseFixture
{

    public Mock<ICategoryRepository> GetRepositoryMock() => new();

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