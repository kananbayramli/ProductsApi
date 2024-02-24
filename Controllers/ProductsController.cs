using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("api/{controller}")]
public class ProductsController : ControllerBase
{
    private readonly ProductsContext _context;

    public ProductsController(ProductsContext context)
    {
        _context = context;
    }



    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        //select after where always
        var products = await _context.Products.Where(p => p.IsActive).Select(p => 
        new ProductDTO{
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price
        }).ToListAsync();
        return Ok(products);
    }



    //[HttpGet("api/{controller}/{id}")]
    [HttpGet("{id}")]
    [Authorize]
    public async  Task<IActionResult> GetProduct( int? id )
    {
        if(id == null)
        {
            return NotFound();
        }

        var p = await _context.Products.Where(x => x.ProductId == id).Select( p => ProductToDTO(p)).FirstOrDefaultAsync();

        if(p == null)
        {
            return NotFound();
        }

        return Ok(p);
    }


    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product entity)
    {
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new{id = entity.ProductId}, entity); //201
    }



    [HttpPut]
    public async Task<IActionResult> UpdateProduct(int id, Product entity)
    {
        if(id != entity.ProductId)
        {
            return BadRequest();
        }

        var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

        if(product == null)
        {
            return NotFound();
        }

        product.ProductName = entity.ProductName;
        product.Price = entity.Price;
        product.IsActive = entity.IsActive;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            
            return NotFound();
        }

        return NoContent();
    }





    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

        if(product ==  null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {     
            return NotFound();
        }

        return NoContent();
    }


    private static ProductDTO ProductToDTO(Product p)
    {
        var entity = new ProductDTO();

        if(p != null)
        {
            entity.ProductId = p.ProductId;
            entity.ProductName = p.ProductName;
            entity.Price = p.Price;
        }

        return entity;
    }

}