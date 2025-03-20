using Crud.Business.Entities;
using Crud.Business.Models.Requests;

namespace Crud.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetAllProducts(PagedRequest pagedRequest);
    }
}
