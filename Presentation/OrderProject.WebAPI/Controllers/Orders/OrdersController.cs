using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderProject.Application.Abstractions.Services;
using OrderProject.Application.Consts;
using OrderProject.Application.Repositories;
using OrderProject.Application.ViewModels.Orders;
using OrderProject.Application.ViewModels.Products;
using OrderProject.Domain.Entities.Orders;
using OrderProject.Domain.Entities.Products;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrderProject.WebAPI.Controllers.Orders
{
    public class OrdersController : BaseApiController
    {
        private readonly IReadRepository<Product> _productRepositories;
        private readonly IWriteRepository<Order> _orderRepositories;
        private readonly IReadRepository<Order> _orderReadRepositories;
        private readonly IRedisService _redisService;
        private readonly IRabbitMqService _rabbitMqService;

        public OrdersController(IReadRepository<Product> productRepositories, IWriteRepository<Order> orderRepositories, IReadRepository<Order> orderReadRepositories, IRabbitMqService rabbitMqService, IRedisService redisService)
        {
            _productRepositories = productRepositories;
            _orderRepositories = orderRepositories;
            _orderReadRepositories = orderReadRepositories;
            _rabbitMqService = rabbitMqService;
            _redisService = redisService;
        }



        [HttpGet("products")]
        public async Task<IActionResult> GetProducts(string category)
        {
            string cacheKey = !string.IsNullOrEmpty(category) ? CacheKeys.Products + category : CacheKeys.Products;
            var products = await _redisService.GetAsync<List<ProductDto>>(CacheKeys.Products);
            if (products == null)
            {
                var query = _mapper.ProjectTo<ProductDto>(_productRepositories.GetAll().Where(x => x.Status));
                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(x => x.Category == category);
                }
                products = query.ToList();

                //cache ekleme işlemi
                await _redisService.SetAsync(cacheKey, products, TimeSpan.FromMinutes(5));
            }

            return Success(products);

        }
        [HttpPost("order")]
        public IActionResult CreateOrder(CreateOrderRequest input)
        {
            var orderEntity = _mapper.Map<Order>(input);
            orderEntity.Id = Guid.NewGuid();
            _orderRepositories.Create(orderEntity);

            //eposta gönderim kuyruğu
            _rabbitMqService.SendEmailQueque(new Application.DTOs.SendEmailQuequeDto 
            {
                Content="Siparişiniz başarıyla alındı",
                Email=input.CustomerEmail,
                Subject="Siparişiniz Hakkında"
            });
            return Success(orderEntity.Id);
        }
    }
}
