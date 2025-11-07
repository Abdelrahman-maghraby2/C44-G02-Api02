using AutoMapper;
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Serves_Abstraction;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Serves
{
    public class ProductServes : IProductServes
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServes(IUnitOfWork unitOfWork , IMapper mapper)
        {
             _unitOfWork = unitOfWork;
           _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands =await _unitOfWork.GetRepository<ProductBrand,int >().GetAllAsync();

            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var Products =await _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(Products); 
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
          var Type =await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Type);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
           var Product =await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(Product);
        }
    }
}
