using System;
using Xunit;
using FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
using System.Threading;
using Moq;
using FluentValidation;

namespace FC.CodeFlix.Catalog.UnitTests.Application.DeleteCategory;

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
        
        var categoryExemple = _fixture.GetValidCategory();

        repositoryMock.Setup(x => x.Get(categoryExemple.Id, It.IsAny<CancellationToken>())).ReturnsAsync(categoryExemple);

        var input = new DeleteCategoryInput(categoryExemple.Id);        
        var useCase = new DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.Get(categoryExemple.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Delete(categoryExemple, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}
