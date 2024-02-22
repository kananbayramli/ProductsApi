using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("api/{controller}")]
public class ProductsController : ControllerBase
{
    private static List<Product>? _products;

    public ProductsController()
    {
        _products = new List<Product>
        {
            new() { ProductId = 1, ProductName = "IPhone 11 Pro Max", Price = 1500, IsActive = true },
            new() { ProductId = 2, ProductName = "IPhone 12 Pro Max", Price = 2000, IsActive = true },
            new() { ProductId = 3, ProductName = "IPhone 13 ", Price = 2500, IsActive = true },
            new() { ProductId = 4, ProductName = "IPhone 14 Pro", Price = 3500, IsActive = true }
        };
    }

    [HttpGet]
    public List<Product> GetProducts()
    {
        return _products ?? new List<Product>();
    }

    //[HttpGet("api/{controller}/{id}")]
    [HttpGet("{id}")]
    public Product GetProduct( int id )
    {
        return _products?.FirstOrDefault( i => i.ProductId == id) ?? new Product();
    }

}