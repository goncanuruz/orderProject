using AutoMapper;
using OrderProject.Application.ViewModels.Orders;
using OrderProject.Application.ViewModels.Products;
using OrderProject.Domain.Entities.Orders;
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
            CreateOrderMaps();
        }
        private void CreateProductMaps()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductDetailDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
        private void CreateOrderMaps()
        {
            CreateMap<CreateOrderRequest, Order>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src =>src.OrderDetails.Sum(x=>x.UnitPrice * x.Amount)));

            CreateMap<CreateOrderDetail, OrderDetail>();
        }
    }
}
