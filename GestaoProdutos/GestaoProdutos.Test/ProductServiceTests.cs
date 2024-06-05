using AutoMapper;
using GestaoDominio.Application.Services;
using GestaoProdutos.Domain.AutoMapper;
using GestaoProdutos.Domain.Dto;
using GestaoProdutos.Domain.Entidades;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Domain.Interfaces.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GestaoProdutos.Test
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

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

        [Fact]
        public async Task AddAsync_DoesNotThrowException_WhenProductAddedSuccessfully()
        {
            // Arrange
            var manufacturingDate = new DateTime(2024, 6, 1, 0, 0, 0);
            var expirationDate = new DateTime(2025, 6, 1, 0, 0, 0);

            var productToAdd = new ProductDto
            {
                Description = "espada",
                ManufacturingDate = manufacturingDate,
                ExpirationDate = expirationDate,
                SupplierCode = "SUP123",
                SupplierDescription = "Fornecedor Teste",
                SupplierCNPJ = "12.345.678/0001-99"
            };

            // Act
            try
            {
                await _productService.AddAsync(productToAdd);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw; 
            }

            // Assert
        }

        [Fact]
        public async Task UpdateAsync_DoesNotThrowException_WhenProductUpdatedSuccessfully()
        {
            // Arrange
            var productId = 1;
            var manufacturingDate = DateTime.Now.AddDays(-1);
            var expirationDate = DateTime.Now.AddDays(30);
            var productToUpdate = new ProductDto
            {
                ProductId = productId,
                Description = "Updated Product",
                ManufacturingDate = manufacturingDate,
                ExpirationDate = expirationDate
            };

            // Act
            try
            {
                await _productService.UpdateAsync(productToUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }

            // Assert
        }

        [Fact]
        public async Task DeleteAsync_DoesNotThrowException_WhenProductDeletedSuccessfully()
        {
            // Arrange
            var productId = 1;

            // Act
            try
            {
                await _productService.DeleteAsync(productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw; 
            }

            // Assert
        }
    }
}
