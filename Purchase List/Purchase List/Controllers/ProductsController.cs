using Microsoft.AspNetCore.Mvc;
using Purchase_List.Models;

namespace Purchase_List.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Bananass", Price = 5m },
                new Product { Id = 2, Name = "IPhonas", Price = 499.99m },
                new Product { Id = 3, Name = "China", Price = 9.597m },
                new Product { Id = 4, Name = "Appelz", Price = 3.99m },
            };
            return products;
        }
    }
}
