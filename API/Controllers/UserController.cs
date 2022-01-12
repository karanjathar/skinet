using API.Data;
using API.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly StoreContext _context;

        public UserController(StoreContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetUsers()
        {
            return _context.Products.ToList();
             
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetUser(int id)
        {
            return _context.Products.Find(id);
             
        }
    }
}
