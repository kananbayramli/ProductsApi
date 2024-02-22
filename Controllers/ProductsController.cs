using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI;

[ApiController]
[Route("api/{controller}")]
public class ProductsController : ControllerBase
{
    private static readonly  string[] Products = {
        "IPhone 14", "IPhone 13 Pro", "IPhone 15"
    };


    [HttpGet]
    public string[] GetProducts()
    {
        return Products;
    }

    //[HttpGet("api/{controller}/{id}")]
    [HttpGet("{id}")]
    public string GetProduct( int id )
    {
        return Products[id];
    }

}