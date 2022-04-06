using FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using System.Collections.Generic;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UpdateCategory;
public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();

        for (int i = 0; i < times; i++)
        {
            var exampleCategory = fixture.GetExempleCategory();
            var exempleInput = new UpdateCategoryInput(exampleCategory.Id, fixture.GetValidCategoryName(), fixture.GetValidCategoryDescription(), fixture.GetRandomBoolean());

            yield return new object[] { exampleCategory, exempleInput };
        }
    }    
}
