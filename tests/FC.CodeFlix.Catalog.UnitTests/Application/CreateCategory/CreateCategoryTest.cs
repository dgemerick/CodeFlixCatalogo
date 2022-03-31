using Moq;
using Xunit;
using FluentAssertions;
using UseCases = FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using System;
using FC.CodeFlix.Catalog.Domain.Entity;
using System.Threading;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.Application.Interfaces;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTest
{
    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var useCase = new UseCases.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = new UseCases.CreateCategoryInput(
            "Category Name",
            "Category Description",
            true
            );

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);

        output.Should().NotBeNull();
        output.Name.Should().Be("Category Name");
        output.Description.Should().Be("Category Description");
        output.IsActive.Should().Be(true);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

    }
}
