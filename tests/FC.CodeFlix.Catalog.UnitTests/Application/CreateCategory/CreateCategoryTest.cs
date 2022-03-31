using Moq;
using Xunit;
using FluentAssertions;
using UseCases = FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using System;
using FC.CodeFlix.Catalog.Domain.Entity;
using System.Threading;
using System.Collections.Generic;
using FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using System.Threading.Tasks;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var useCase = new UseCases.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstatiateAggregate))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(nameof(GetInvalidInputs))]
    public async void ThrowWhenCantInstatiateAggregate(CreateCategoryInput input, string exceptionMessage)
    {
        var useCase = new UseCases.CreateCategory(_fixture.GetRepositoryMock().Object, _fixture.GetUnitOfWorkMock().Object);

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }

    private static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputsList = new List<object[]>();

        var invalidInputShortName = fixture.GetInput();
        invalidInputShortName.Name = invalidInputShortName.Name[..2];
        invalidInputsList.Add(new object[]
        {
            invalidInputShortName,
            "Name should be at leats 3 characters long"
        });

        var invalidInputTooLongName = fixture.GetInput();
        var tooLongNameForCategory = fixture.Faker.Commerce.ProductName();
        while (tooLongNameForCategory.Length <= 255)
            tooLongNameForCategory = $"{tooLongNameForCategory} {fixture.Faker.Commerce.ProductName()}";

        invalidInputTooLongName.Name = tooLongNameForCategory;

        invalidInputsList.Add(new object[]
        {
            invalidInputTooLongName,
            "Name should be less 255 characters long"
        });

        return invalidInputsList;
    }
}
