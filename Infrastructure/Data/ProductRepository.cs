using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //To make this injectable into other classes (ie controllers), this needs to be added as a service into our startup class
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
           
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        //the repository will interact with the store context and then the controllers use the repository methods.
        //In order to retrieve the data from our DB, so our repo is abstracting our data acess methods
        public async Task<Product> GetProductByIdAsync(int Id)
        {
            //throw new NotImplementedException();
            return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}