
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategoryInput : IRequest<GetCategoryOutput>
{
    public GetCategoryInput(Guid id) => Id = id;

    public Guid Id { get; set; }
    


}
