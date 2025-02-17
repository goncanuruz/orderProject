using AutoMapper;
using OrderProject.Application.ViewModels.Products;
using OrderProject.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.MappingProfiles
{
    public class OrderProjectMapperProfile : Profile
    {
        public OrderProjectMapperProfile()
        {
            CreateProductMaps();
        }
        private void CreateProductMaps()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductDetailDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
