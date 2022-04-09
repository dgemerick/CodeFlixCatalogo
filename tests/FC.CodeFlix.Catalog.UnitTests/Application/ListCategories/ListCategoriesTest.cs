﻿using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.ListCategories;

[Collection(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTest
{
    private readonly ListCategoriesTestFixture _fixture;

    public ListCategoriesTest(ListCategoriesTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(List))]
    [Trait("Application", "ListCategories - use Cases")]
    public async Task List()
    {
        var categoriesExampleList = _fixture.GetExampleCategoriesList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = ListCategoriesInput(
            page: 2,
            perPage: 15,
            search: "search-example",
            sort: "name",
            dir: SearchOrder.Asc
        );
        var outputRepositorySearch = new OutputSearch<Category>(
            currentPage: input.Page,
            perPage: input.PerPage,
            Items: (IReadOnlyList<Category>)categoriesExampleList,
            Total: 70
        );
        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput.Page == input.Page &&
                searchInput.PerPage == input.PerPage &&
                searchInput.Search == input.Search &&
                searchInput.OrderBy == input.PerPage &&
                searchInput.Order == input.Order &&
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        //Asserts
        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.Page);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        output.Items.Foreach(outputItem =>
        {
            var repositoryCategory = outputRepositorySearch.Items.Find(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryCategory.Name);
            outputItem.Description.Should().Be(repositoryCategory.Description);
            outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
            outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
        });

        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput.Page == input.Page &&
                searchInput.PerPage == input.PerPage &&
                searchInput.Search == input.Search &&
                searchInput.OrderBy == input.PerPage &&
                searchInput.Order == input.Order &&
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);

    }
}
