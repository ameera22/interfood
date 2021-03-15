using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context) //inject store context into constructor - injection gives the context a lifetime. Controller is created everytime new request is made and new context is a dependency. 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //synchronous requests block thread until one method is completed and waits till request to sql is finished and data comes back. 
            //asynchronous methods dont wait for one result to come back before initiating tasks. It passes request off to a delegate which will then go run the db query and in the meantime the request the thread is running on can handle other things. 
            //Once delegate has finished task it notifies method/request and picks up results and carries on. 
            //Makes application more scalable. 

            var products = await _context.Products.ToListAsync();
            return Ok(products);            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}