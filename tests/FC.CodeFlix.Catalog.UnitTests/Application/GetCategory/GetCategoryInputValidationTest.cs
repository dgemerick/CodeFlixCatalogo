using FC.CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;
using System;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryInputValidationTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryInputValidationTest(GetCategoryTestFixture fixture) => _fixture = fixture;

    [Fact(DisplayName = nameof(ValidationOk))]
    [Trait("Application", "GetCategoryInputValidationTest - UseCases")]
    public void ValidationOk()
    {
        var validInput = new GetCategoryInput(Guid.NewGuid());
        var validator = new GetCategoryInputValidatior();

        var validationResult = validator.Validate(validInput);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(InvalidWhenEmptyGuidId))]
    [Trait("Application", "GetCategoryInputValidationTest - UseCases")]
    public void InvalidWhenEmptyGuidId()
    {
        var invalidInput = new GetCategoryInput(Guid.Empty);
        var validator = new GetCategoryInputValidatior();

        var validationResult = validator.Validate(invalidInput);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
    }
}
