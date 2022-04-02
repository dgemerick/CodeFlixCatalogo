using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CreateCategoryOutput>
{
    
}
