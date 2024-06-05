using GestaoProdutos.Domain.Entidades;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoProdutos.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> ListAsync(ProductQueryParams queryParams)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(queryParams.Description))
                query = query.Where(p => p.Description.Contains(queryParams.Description));

            if (!string.IsNullOrEmpty(queryParams.Status))
                query = query.Where(p => p.Status == queryParams.Status);

            if (queryParams.PageNumber.HasValue && queryParams.PageSize.HasValue)
                query = query.Skip((queryParams.PageNumber.Value - 1) * queryParams.PageSize.Value).Take(queryParams.PageSize.Value);

            return await query.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Status = "Inativo";
                await _context.SaveChangesAsync();
            }
        }
    }

}
