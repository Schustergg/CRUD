using AutoMapper;
using Crud.API.Controllers;
using Crud.API.ViewModels;
using Crud.Business.Entities;
using Crud.Business.Interfaces;
using Crud.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;

public class ProductsControllerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<INotifier> _mockNotifier;
    private readonly Mock<IUser> _mockUser;
    private readonly Mock<IDistributedCacheService> _mockDistributedCache;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockProductService = new Mock<IProductService>();
        _mockMapper = new Mock<IMapper>();
        _mockNotifier = new Mock<INotifier>();
        _mockUser = new Mock<IUser>();
        _mockDistributedCache = new Mock<IDistributedCacheService>();

        _controller = new ProductsController(
            _mockNotifier.Object,
            _mockMapper.Object,
            _mockUser.Object,
            _mockDistributedCache.Object,
            _mockProductRepository.Object,
            _mockProductService.Object
        );
    }

    [Fact]
    public async Task Add_ShouldReturnOk()
    {
        // Arrange
        var productViewModel = new ProductViewModel { Name = "Test Product", Price = 10 };
        var product = Product.Create("Test Product", 10);

        // Mock repository and service
        _mockProductRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(product);
        _mockProductService.Setup(service => service.Add(It.IsAny<Product>())).Returns(Task.CompletedTask);
        _mockMapper.Setup(mapper => mapper.Map<Product>(It.IsAny<ProductViewModel>())).Returns(product);

        // Act
        var response = await _controller.Add(productViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Update_ShouldReturnOk()
    {
        // Arrange
        
        var productViewModel = new ProductViewModel { Name = "Updated Product", Price = 10 };
        var product = Product.Create("Updated Product", 10);
        productViewModel.Id = product.Id;
        var productId = product.Id;

        // Mock repository and service
        _mockProductRepository.Setup(repo => repo.GetById(It.Is<Guid>(id => id == productId)))
                         .ReturnsAsync(product);

        _mockProductService.Setup(service => service.Update(It.IsAny<Product>())).Returns(Task.CompletedTask);

        _mockMapper.Setup(mapper => mapper.Map<ProductViewModel>(It.IsAny<Product>())).Returns(productViewModel);

        // Act
        var response = await _controller.Update(productId, productViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ShouldReturnOk()
    {
        // Arrange
        var productViewModel = new ProductViewModel { Name = "Product to Delete" , Price = 10 };
        var product = Product.Create("Product to Delete", 10);
        productViewModel.Id = product.Id;
        var productId = product.Id;

        // Mock repository and service
        _mockProductRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(product);
        _mockProductService.Setup(service => service.Delete(It.IsAny<Product>())).Returns(Task.CompletedTask);
        _mockMapper.Setup(mapper => mapper.Map<ProductViewModel>(It.IsAny<Product>())).Returns(productViewModel);

        // Act
        var result = await _controller.Delete(productId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}
