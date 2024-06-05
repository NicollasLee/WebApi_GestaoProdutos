using AutoMapper;
using GestaoProdutos.Domain.Dto;
using GestaoProdutos.Domain.Entidades;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoDominio.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> ListAsync(ProductQueryParams queryParams)
        {
            var products = await _productRepository.ListAsync(queryParams);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task AddAsync(ProductDto productDto)
        {
            ValidateProductDates(productDto);
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            ValidateProductDates(productDto);
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private void ValidateProductDates(ProductDto productDto)
        {
            if (productDto.ManufacturingDate >= productDto.ExpirationDate)
            {
                throw new ArgumentException("A data de fabricação não pode ser maior ou igual à data de validade.");
            }
        }
    }

}
