using FluentValidation;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategoryInputValidatior : AbstractValidator<GetCategoryInput>
{
    public GetCategoryInputValidatior() => RuleFor(x => x.Id).NotEmpty();


}
