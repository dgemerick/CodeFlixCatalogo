using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using UseCase = FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = (nameof(UpdateCategory)))]
    [Trait("Application", "UpdateCategory - Use Cases")]
    public async Task UpdateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var exampleCategory = _fixture.GetExempleCategory();
        repositoryMock.Setup(x => x.Get(exampleCategory.Id, It.IsAny<CancellationToken>())).ReturnsAsync(exampleCategory);
        var input = UseCase.UpdateCategoryInput(exampleCategory.Id, _fixture.GetValidCategoryName(), _fixture.GetValidCategoryDescription(), !exampleCategory.IsActive);
        var useCase = new UseCase.UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        //act
        CategoryModelOutput output = await useCase.Handle(input);

        //
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        repositoryMock.Verify(x => x.Get(exampleCategory.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Update(exampleCategory, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);

    }

}
