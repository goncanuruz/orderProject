using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderProject.Application.Repositories;
using OrderProject.Application.ViewModels.Orders;
using OrderProject.Application.ViewModels.Products;
using OrderProject.Domain.Entities.Orders;
using OrderProject.Domain.Entities.Products;
using System.Text;

namespace OrderProject.WebAPI.Controllers.Orders
{
    public class SeedController : BaseApiController
    {
        private readonly IWriteRepository<Product> _productWriteRepositories;
        private readonly IReadRepository<Product> _productReadRepositories;

        public SeedController(IWriteRepository<Product> productWriteRepositories, IReadRepository<Product> productReadRepositories)
        {
            _productWriteRepositories = productWriteRepositories;
            _productReadRepositories = productReadRepositories;
        }

        [HttpPost("products")]
        public IActionResult SeedProducts() 
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "products.json");
            using StreamReader reader = new(fullPath, Encoding.GetEncoding("utf-8"), false);
            var json = reader.ReadToEnd();
            var products = JsonConvert.DeserializeObject<List<Product>>(json);

            foreach (var product in products)
            {
                if (_productReadRepositories.GetById(product.Id)==null)
                {
                    _productWriteRepositories.Create(product);
                    Console.WriteLine($"{product.Description} added");
                }
            }
            return Success();
        }
    
    }
}
