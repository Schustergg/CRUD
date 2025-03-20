using Crud.Business.Entities;
using Crud.Business.Interfaces;
using Crud.Business.Models.Requests;
using Crud.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CrudDbContext context) : base(context) { }

        public async Task<PagedResult<Product>> GetAllProducts(PagedRequest pagedRequest)
        {
            if (pagedRequest.PageNumber < 1)
                pagedRequest.PageNumber = 1;

            if (pagedRequest.PageSize < 1)
                pagedRequest.PageSize = 10;

            var query = Db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(pagedRequest.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(pagedRequest.SearchTerm));
            }

            if (!string.IsNullOrEmpty(pagedRequest.SortBy))
            {
                if (pagedRequest.SortDirection.ToUpper() == "DESC")
                {
                    query = query.OrderByDescending(p => EF.Property<object>(p, pagedRequest.SortBy));
                }
                else
                {
                    query = query.OrderBy(p => EF.Property<object>(p, pagedRequest.SortBy));
                }
            }

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((pagedRequest.PageNumber - 1) * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = products,
                TotalCount = totalCount,
                PageNumber = pagedRequest.PageNumber,
                PageSize = pagedRequest.PageSize
            };
        }

    }
}
