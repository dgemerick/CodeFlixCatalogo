using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.EndToEndTests.Api.Category.Common;
public class CategoryPersistence
{
    private readonly CodeflixCatalogDbContext _context;

    public CategoryPersistence(CodeflixCatalogDbContext context)
    {
        _context = context;
    }

    public async Task<DomainEntity.Category?> GetById(Guid id) 
        => await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
}
