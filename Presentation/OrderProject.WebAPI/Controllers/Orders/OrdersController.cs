﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProject.Application.Repositories;
using OrderProject.Application.ViewModels.Orders;
using OrderProject.Application.ViewModels.Products;
using OrderProject.Domain.Entities.Orders;
using OrderProject.Domain.Entities.Products;

namespace OrderProject.WebAPI.Controllers.Orders
{
    public class OrdersController : BaseApiController
    {
        private readonly IReadRepository<Product> _productRepositories;
        private readonly IWriteRepository<Order> _orderRepositories;
        private readonly IReadRepository<Order> _orderReadRepositories;
        public OrdersController(IReadRepository<Product> productRepositories, IWriteRepository<Order> orderRepositories, IReadRepository<Order> orderReadRepositories)
        {
            _productRepositories = productRepositories;
            _orderRepositories = orderRepositories;
            _orderReadRepositories = orderReadRepositories;
        }



        [HttpGet("products")]
        public IActionResult GetProducts() 
        {
            //todo : cache eklenecek
            var result = _mapper.ProjectTo<ProductDto>(_productRepositories.GetAll().Where(x=>x.Status));
            return Success(result);
        }
        [HttpPost("order")]
        public IActionResult CreateOrder(CreateOrderRequest input)
        {
            var orderEntity = _mapper.Map<Order>(input);
            orderEntity.Id = Guid.NewGuid();
            _orderRepositories.Create(orderEntity);

            //todo : mail gönderimi
            return Success(orderEntity.Id);
        }
    }
}
