using Crud.Business.Entities;
using Crud.Business.Interfaces;

namespace Crud.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
                              INotifier notificador) : base(notificador)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {

            if (!product.IsValid())
            {
                Notificar(product.Validation);
                return;
            }


            await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!product.IsValid())
            {
                Notificar(product.Validation);
                return;
            }

            await _productRepository.Update(product);
        }

        public async Task Delete(Product product)
        {
            await _productRepository.Delete(product);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
