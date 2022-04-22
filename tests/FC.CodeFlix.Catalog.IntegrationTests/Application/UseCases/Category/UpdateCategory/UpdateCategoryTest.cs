using FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.CodeFlix.Catalog.Infra.Data.EF;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using DomainModel = FC.CodeFlix.Catalog.Domain.Entity;
using ApplicationUseCase = FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = (nameof(UpdateCategory)))]
    [Trait("Integration/Application", "UpdateCategory - Use Cases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 5,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateCategory(DomainModel.Category exampleCategory, UpdateCategoryInput input)
    {
        var dbContext = _fixture.CreateDbContext();
        await dbContext.AddRangeAsync(_fixture.GetExampleCategoriesList());
        var trakingInfo = await dbContext.AddAsync(exampleCategory);
        dbContext.SaveChanges();
        trakingInfo.State = EntityState.Detached;
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCase.UpdateCategory(repository, unitOfWork);
        
        //act
        var output = await useCase.Handle(input, CancellationToken.None);

        //
        var dbCategory = await (_fixture.CreateDbContext(true)).Categories.FindAsync(output.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(input.Name);
        dbCategory.Description.Should().Be(input.Description);
        dbCategory.IsActive.Should().Be((bool)input.IsActive!);
        dbCategory.CreatedAt.Should().Be(output.CreatedAt);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be((bool)input.IsActive!);
        
    }
}
