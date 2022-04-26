﻿using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
using FC.CodeFlix.Catalog.Infra.Data.EF;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ApplicationUseCase = FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.DeleteCategory;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest
{
    private readonly DeleteCategoryTestFixture _fixture;

    public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("Integration/Application", "DeleteCategory - Use Cases")]
    public async Task DeleteCategory()
    {
        var dbContext = _fixture.CreateDbContext();
        var categoryExample = _fixture.GetExampleCategory();
        var exampleList = _fixture.GetExampleCategoriesList(10);
        await dbContext.AddRangeAsync(exampleList);
        var traking = await dbContext.AddAsync(categoryExample);
        await dbContext.SaveChangesAsync();
        traking.State = EntityState.Detached;
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCase.DeleteCategory(repository, unitOfWork);
        var input = new DeleteCategoryInput(categoryExample.Id);

        await useCase.Handle(input, CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var dbCategoryDeleted = await assertDbContext.Categories.FindAsync(categoryExample.Id);
        dbCategoryDeleted.Should().BeNull();
        var dbCategories = await assertDbContext.Categories.ToListAsync();
        dbCategories.Should().HaveCount(exampleList.Count);
    }

    [Fact(DisplayName = nameof(DeleteCategoryThrowsWhenNotFound))]
    [Trait("Integration/Application", "DeleteCategory - Use Cases")]
    public async Task DeleteCategoryThrowsWhenNotFound()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleList = _fixture.GetExampleCategoriesList(10);
        await dbContext.AddRangeAsync(exampleList);
        await dbContext.SaveChangesAsync();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCase.DeleteCategory(repository, unitOfWork);
        var input = new DeleteCategoryInput(Guid.NewGuid());

        var task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{input.Id}' not found.");
    }
}
