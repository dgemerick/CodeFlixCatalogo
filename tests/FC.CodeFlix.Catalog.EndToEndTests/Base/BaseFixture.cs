
using Bogus;
using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.EndToEndTests.Base;
public class BaseFixture
{
    protected Faker Faker { get; set; }

    public ApiClient ApiClient { get; set; }

    public BaseFixture() => Faker = new Faker("pt_BR");

    public CodeflixCatalogDbContext CreateDbContext()
    {
        var context = new CodeflixCatalogDbContext(
            new DbContextOptionsBuilder<CodeflixCatalogDbContext>()
                .UseInMemoryDatabase("end2end-tests-db")
                .Options
        );

        return context;
    }
}
