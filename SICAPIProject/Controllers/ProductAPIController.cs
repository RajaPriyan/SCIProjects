using Microsoft.AspNetCore.Mvc;
using SICAPIProject.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SICAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        // GET: api/<ProductAPIController>
        // In-memory storage for products
        private static List<ProductVM> _products = new List<ProductVM>
        {
            new ProductVM { ProductID = 1, Name = "The Great Gatsby", Category = "Books", Quantity = 30, Price = 10.99M },
            new ProductVM { ProductID = 2, Name = "1984", Category = "Books", Quantity = 50, Price = 8.99M },
            new ProductVM { ProductID = 3, Name = "To Kill a Mockingbird", Category = "Books", Quantity = 40, Price = 12.49M },
            new ProductVM { ProductID = 4, Name = "The Catcher in the Rye", Category = "Books", Quantity = 20, Price = 14.00M },
            new ProductVM { ProductID = 5, Name = "Pride and Prejudice", Category = "Books", Quantity = 60, Price = 9.49M },
            new ProductVM { ProductID = 6, Name = "Moby-Dick", Category = "Books", Quantity = 25, Price = 11.00M },
            new ProductVM { ProductID = 7, Name = "War and Peace", Category = "Books", Quantity = 15, Price = 15.99M },
            new ProductVM { ProductID = 8, Name = "Crime and Punishment", Category = "Books", Quantity = 35, Price = 13.50M },
            new ProductVM { ProductID = 9, Name = "Brave New World", Category = "Books", Quantity = 45, Price = 7.99M },
            new ProductVM { ProductID = 10, Name = "The Odyssey", Category = "Books", Quantity = 18, Price = 17.49M }
        };


        // GET: api/<ProductAPIController>?page=1&size=10
        [HttpGet]
        public IActionResult GetProducts([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string category = null, [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null)
        {
            // Apply filters
            var filteredProducts = _products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                filteredProducts = filteredProducts.Where(p => p.Category.ToLower() == category.ToLower());

            if (minPrice.HasValue)
                filteredProducts = filteredProducts.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice.Value);

            // Pagination logic
            var paginatedProducts = filteredProducts
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return Ok(paginatedProducts);
        }

        // GET: api/<ProductAPIController>/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        // POST: api/<ProductAPIController>
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductVM newProduct)
        {
            if (newProduct == null)
                return BadRequest("Product data is required");

            if (_products.Any(p => p.ProductID == newProduct.ProductID))
                return Conflict("Product ID already exists");

            _products.Add(newProduct);

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductID }, newProduct);
        }

        // PUT: api/<ProductAPIController>/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductVM updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductID == id);

            if (existingProduct == null)
                return NotFound("Product not found");

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Category = updatedProduct.Category;
            existingProduct.Quantity = updatedProduct.Quantity;
            existingProduct.Price = updatedProduct.Price;

            return NoContent(); // 204 No Content
        }

        // DELETE: api/<ProductAPIController>/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
                return NotFound("Product not found");

            _products.Remove(product);

            return NoContent(); // 204 No Content
        }
    }
}
