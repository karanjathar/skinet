using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    

   
    public class ProductController:BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;


        //private readonly IProductRepository _repo;

        //public ProductController(IProductRepository repo)
        //{

        //    _repo = repo;
        //}

        public ProductController(IGenericRepository<Product> ProductsRepo, IGenericRepository<ProductBrand> ProductBrandRepo, IGenericRepository<ProductType> ProductTypeRepo,IMapper mapper)
        {
            _productsRepo = ProductsRepo;
            _productBrandRepo = ProductBrandRepo;
            _productTypeRepo = ProductTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
       public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
           [FromQuery]ProductSpecParams productparams)
       {
            var spec = new ProductWithTypesBrandsSpecification(productparams);
            // var products = await _repo.GetProductsAsync();
            var products = await _productsRepo.ListAsync(spec);
            var countSpec = new ProductWithFiltersCountWithSpecification(productparams);

            var totalItems = await _productsRepo.CoutAsync(countSpec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);


            return Ok(new Pagination<ProductToReturnDto>(productparams.PageIndex, productparams.PageSize,totalItems,data));
       } 


       [HttpGet("{id}")]
       [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
       {
            var spec = new ProductWithTypesBrandsSpecification(id);
            var product= await _productsRepo.GetEntityWithSpec(spec);

            if (product == null)
            {
                return NotFound(new ApiResponse(404)); 
            }

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