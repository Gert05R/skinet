using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.DTO;
using AutoMapper;
using API.Errors;

//https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136692#questions/12487512
namespace API.Controllers
{
    
    //https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136714#questions
    //These methods are updated to return the data from the DB
    //By injecting storecontext, this gives us access to the methods in the class.
    //creates a new instance of storecontext
    public class ProductsController : BaseAPiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        private readonly IMapper _mapper;
       

        //Initialize field from parameter (_context is initialized)
        //https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136752#questions


    

        //Obsolete
        //public ProductsController(StoreContext context)
        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> 
        productTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;

            //_context = context; //Is an instance of the db

        }

        [HttpGet]

        //Actionresult is what we return ffrom the controller: controllerBase
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts() {
            
            var spec = new ProductsWithTypesAndBrandsSpecification();
            
            //var products = await _context.Products.ToListAsync(); //return products from the instance _context
            var products = await _productsRepo.ListAsync(spec);

            //return Ok(products);

            /*return products.Select(product => new ProductToReturnDto 
             {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            }).ToList();*/

            return Ok(_mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        //Swagger responses
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) {

            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            //return await _context.Products.FindAsync(id);
            //return await _productsRepo.GetEntityWithSpec(spec);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            //Swagger documentation
            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }

    }
}