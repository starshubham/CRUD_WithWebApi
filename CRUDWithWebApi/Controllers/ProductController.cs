using CRUDWithWebApi.DataAccessLayer;
using CRUDWithWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public ProductController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _context.Products.ToList();
                if (products.Count == 0)
                {
                    return NotFound("No Product Available.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(p => p.Id == id); // or _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product details not found with id {id}");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Product model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Product Created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Product model)
        {
            if (model == null || model.Id == 0)
            {
                if(model == null)
                {
                    return BadRequest("Model Data is Invalid.");
                }
                else if (model.Id == 0)
                {
                    return BadRequest($"Produt Id {model.Id} is Invalid.");
                }
            }

            try
            {
                var product = _context.Products.Find(model.Id);
                if (product == null)
                {
                    return NotFound($"Product not found with Id {model.Id}");
                }
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _context.SaveChanges();
                return Ok("Product details updated successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product not found with id {id}");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok("Product details removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
