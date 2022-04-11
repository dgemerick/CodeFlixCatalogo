using System;
using Xunit;
using UseCase = FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
using System.Threading;
using Moq;
using FluentValidation;
using System.Threading.Tasks;
using FC.CodeFlix.Catalog.Application.Exceptions;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.DeleteCategory;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest
{
    private readonly DeleteCategoryTestFixture _fixture;

    public DeleteCategoryTest(DeleteCategoryTestFixture fixture) => _fixture = fixture;

    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("Application", "DeleteCategory - Use Cases")]
    public async Task DeleteCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var categoryExemple = _fixture.GetExampleCategory();

        repositoryMock.Setup(x => x.Get(categoryExemple.Id, It.IsAny<CancellationToken>())).ReturnsAsync(categoryExemple);

        var input = new UseCase.DeleteCategoryInput(categoryExemple.Id);
        var useCase = new UseCase.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.Get(categoryExemple.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Delete(categoryExemple, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
    [Trait("Application", "DeleteCategory - Use Cases")]
    public async Task ThrowWhenCategoryNotFound()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var exempleGuid = Guid.NewGuid();

        repositoryMock.Setup(x => x.Get(exempleGuid, It.IsAny<CancellationToken>())).ThrowsAsync(new NotFoundException($"Category '{exempleGuid}' not found."));

        var input = new UseCase.DeleteCategoryInput(exempleGuid);
        var useCase = new UseCase.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(x => x.Get(exempleGuid, It.IsAny<CancellationToken>()), Times.Once);
    }
}
