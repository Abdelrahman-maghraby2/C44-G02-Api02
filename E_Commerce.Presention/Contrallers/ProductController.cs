using E_Commerce.Serves_Abstraction;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presention.Contrallers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServes _productServes;

        public ProductController(IProductServes productServes)
        {
          _productServes = productServes;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productServes.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO >> GetProduct(int  id)
        {
            var products = await _productServes.GetProductByIdAsync(id);
            return Ok(products);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var Brands = await _productServes.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        { 
            var Type =await _productServes.GetAllTypesAsync();
            return Ok(Type);
        }
    }
}
