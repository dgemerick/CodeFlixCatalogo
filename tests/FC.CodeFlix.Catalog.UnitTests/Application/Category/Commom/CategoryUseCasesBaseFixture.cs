using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;
using System;
using DomainModel = FC.CodeFlix.Catalog.Domain.Entity;


namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.Commom;

public abstract class CategoryUseCasesBaseFixture : BaseFixture
{
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

    public bool GetRandomBoolean() => new Random().NextDouble() < 0.5;

    public Mock<ICategoryRepository> GetRepositoryMock() => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public DomainModel.Category GetExampleCategory()
    => new DomainModel.Category(
        GetValidCategoryName(),
        GetValidCategoryDescription(),
        GetRandomBoolean()
    );
}
