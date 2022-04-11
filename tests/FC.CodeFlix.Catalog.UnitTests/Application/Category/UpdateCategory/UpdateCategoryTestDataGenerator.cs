using System.Collections.Generic;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.UpdateCategory;
public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();

        for (int i = 0; i < times; i++)
        {
            var exampleCategory = fixture.GetExampleCategory();
            var exempleInput = fixture.GetValidInput(exampleCategory.Id);

            yield return new object[] { exampleCategory, exempleInput };
        }
    }

    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        //
        var fixture = new UpdateCategoryTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int i = 0; i < times; i++)
        {
            switch (i % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInvalidInputShortName(),
                        "Name should be at least 3 characters long"
                    });
                    break;
                case 1:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInvalidInputTooLongName(),
                        "Name should be less 255 characters long"
                    });
                    break;
                case 2:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInvalidInputTooLongDescription(),
                        "Description should be less 10000 characters long"
                    });
                    break;
                default:
                    break;
            }
        }

        return invalidInputsList;
    }
}
