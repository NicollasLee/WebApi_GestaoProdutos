using AutoMapper;
using GestaoDominio.Application.Services;
using GestaoProdutos.Domain.AutoMapper;
using GestaoProdutos.Domain.Entidades;
using GestaoProdutos.Domain.Interfaces.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GestaoProdutos.Test
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _productService = new ProductService(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var product = new Product { ProductId = productId, Description = "Product 1" };
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
        }
    }

}
