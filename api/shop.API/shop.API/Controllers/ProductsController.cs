using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using shop.API.Data;
using shop.API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace shop.API.Controllers
{
 [ApiController]

    /* an attribute that specifies the route template for this controller. 
     * This means that any requests that start with “api/Products” will be handled by this controller
     */
    [Route("api/[controller]")]

    // The Controller base class provides methods and properties for creating responses, validating models, handling errors
    public class ProductsController : Controller
    {

        /* private field that holds an instance of ShopDbContext. ShopDbContext is a class that represents the database 
         * context for accessing products data using Entity Framework Core, which is an object-relational mapper (ORM) framework
         */
        private readonly ShopDbContext _shopDbContext;

        //This way, the controller can use dependency injection to get access to the database context
    public ProductsController(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        [HttpGet]
        // IActionResult is an interface that represents various types of action results, such as Ok, NotFound, BadRequest, etc
        public async Task<IActionResult> GetAllProducts()
        {
          /*uses the _shopDbContext field to query the Products table in the database and convert it to a list 
           asynchronously using LINQ (Language Integrated Query) methods */
          var products = await _shopDbContext.Products.ToListAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product productRequest)
        {
            productRequest.Id = Guid.NewGuid();

            await _shopDbContext.Products.AddAsync(productRequest);
            await _shopDbContext.SaveChangesAsync();

            return Ok(productRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var product = await _shopDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, Product updateProductRequest)
        {
           var product = await _shopDbContext.Products.FindAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            product.Name = updateProductRequest.Name;
            product.Description = updateProductRequest.Description; 
            product.Category = updateProductRequest.Category;
            product.Price = updateProductRequest.Price;

            await _shopDbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var product = await _shopDbContext.Products.FindAsync(id);

            if(product == null)
            {
                return NotFound();
            }
            _shopDbContext.Products.Remove(product);
            await _shopDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
