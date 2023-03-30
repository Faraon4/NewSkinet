using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // Repository will interact with StoreContext
    // And Controller will interact with the repository


    public class ProductRepository : IProductRepos
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public Task<ProductBrand> GetProductBrandByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            // We add eager loading, to display the brand and type of each product when we query it individually
           return await _context.Products
                                .Include(p => p.ProductType)
                                .Include(p => p.ProductBrand)
                                .FirstOrDefaultAsync( p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            // We add eager loading, to display the brand and type of each products when we query them
            return await _context.Products
                                 .Include(p => p.ProductType)
                                 .Include(p => p.ProductBrand)
                                 .ToListAsync();
        }

        public Task<ProductType> GetProductTypeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}