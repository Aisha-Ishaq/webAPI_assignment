using Microsoft.AspNetCore.Http;
using Watchzen_Api.Data;
using Microsoft.AspNetCore.Mvc;
using Watchzen_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Extensions;

namespace Watchzen_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Appdbcontext _dbContext;

        public ProductController(Appdbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductInfo>> GetUsers()
        {
            return await _dbContext.Product.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var product = await _dbContext.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] ProductInfo request)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists in the database
                if (await _dbContext.Product.AnyAsync(u => u.Id == request.Id))
                {
                    // User already exists
                    return Conflict();
                }

                // Create a new user in the database
                var product = new ProductInfo
                {
                    Name = request.Name,
                    Category = request.Category,
                    Color = request.Color,
                    Price = request.Price,
                };
                _dbContext.Product.Add(product);

                await _dbContext.SaveChangesAsync();

                // Return a 201 Created status code
                return Created(new Uri(Request.GetEncodedUrl()), product);
            }

            // Bad request - invalid input data
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] ProductInfo request)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists in the database
                var product = await _dbContext.Product.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                // Update the user in the database
                product.Name = request.Name;
                product.Category = request.Category;
                product.Color = request.Color;
                product.Price = request.Price;

                _dbContext.Product.Update(product);
                await _dbContext.SaveChangesAsync();

                // Return a 200 OK status code
                return Ok();
            }

            // Bad request - invalid input data
            return BadRequest(ModelState);
        }
    }
}
