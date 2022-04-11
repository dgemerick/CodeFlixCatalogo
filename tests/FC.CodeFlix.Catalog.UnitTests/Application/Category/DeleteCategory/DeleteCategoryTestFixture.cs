using FC.CodeFlix.Catalog.UnitTests.Application.Category.Commom;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.DeleteCategory;

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture> { }

public class DeleteCategoryTestFixture : CategoryUseCasesBaseFixture
{ }
