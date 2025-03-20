using AutoMapper;
using Crud.API.ViewModels;
using Crud.Business.Entities;
using Crud.Business.Interfaces;
using Crud.Business.Models.Requests;
using Crud.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductsController : MainController
    {
        private readonly IProductRepository productRepository;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IDistributedCacheService distributedCacheService;

        public ProductsController(
            INotifier notifier,
            IMapper mapper,
            IUser user,
            IDistributedCacheService distributedCacheService,
            IProductRepository productRepository,
            IProductService productService) : base(notifier, user)
        {
            this.mapper = mapper;
            this.distributedCacheService = distributedCacheService;
            this.productRepository = productRepository;
            this.productService = productService;
        }
      

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductViewModel>>> GetAllPaged([FromQuery] PagedRequest request)
        {
            var pagedProducts = await productRepository.GetAllProducts(request);

            var productViewModels = mapper.Map<IEnumerable<ProductViewModel>>(pagedProducts.Items);

            return Ok(new PagedResult<ProductViewModel>
            {
                Items = productViewModels,
                TotalCount = pagedProducts.TotalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null) return NotFound();

            return productViewModel;
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var product = mapper.Map<Product>(productViewModel);
            await productService.Add(product);

            productViewModel.Id = product.Id;
            return CustomResponse(productViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Update(Guid id, ProductViewModel productViewModel)
        {

            var produtoAtualizacao = await GetProduct(id);
            if (produtoAtualizacao == null) return NotFound();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            productViewModel.Id = id;
            await productService.Update(mapper.Map<Product>(productViewModel));

            return CustomResponse(productViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await GetProduct(id);
            if (produto == null) return NotFound();

            await productService.Delete(mapper.Map<Product>(produto));

            return CustomResponse();
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var cachedProduct = 
                await distributedCacheService.GetRecordAsync<ProductViewModel>($"Product-{id}");

            if (cachedProduct != null)
                return cachedProduct;

            var product = await productRepository.GetById(id);
            var productViewModel = mapper.Map<ProductViewModel>(product);
            await distributedCacheService.SetRecordAsync($"Product-{id}", productViewModel);

            return productViewModel;
        }

    }
}
