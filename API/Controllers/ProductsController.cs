using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

//https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136692#questions/12487512
namespace API.Controllers
{
    [ApiController]
    //the controller in square brackets is just a placeholder for what the class name is called, in this case thus "Products"
    [Route("api/[controller]")]
    //https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136714#questions
    //These methods are updated to return the data from the DB
    //By injecting storecontext, this gives us access to the methods in the class.
    //creates a new instance of storecontext
    public class ProductsController : ControllerBase
    {
        //Initialize field from parameter (_context is initialized)
        //https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136752#questions
        
        private readonly IProductRepository _repo;
        
        //Obsolete
        //public ProductsController(StoreContext context)
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
            //_context = context; //Is an instance of the db
            
        }

        [HttpGet]

        //Actionresult is what we return ffrom the controller: controllerBase
        public async Task<ActionResult<List<Product>>> GetProducts() {
            
            //var products = await _context.Products.ToListAsync(); //return products from the instance _context
            var products = await _repo.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        
        public async Task<ActionResult<Product>> GetProduct(int id) {

            //return await _context.Products.FindAsync(id);
            return await _repo.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync());
        }

    }
}