using Crud.Business.Entities;

namespace Crud.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
