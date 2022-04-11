using FC.CodeFlix.Catalog.UnitTests.Application.Category.Commom;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture>
{

}

public class GetCategoryTestFixture : CategoryUseCasesBaseFixture
{ }
