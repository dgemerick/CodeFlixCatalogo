using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput>
{
}
