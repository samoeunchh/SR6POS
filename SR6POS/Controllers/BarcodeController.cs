using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SR6POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SR6POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BarcodeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var product = await (from p in _context.Product
                                 join pp in _context.ProductPrice on p.SaleUnit equals pp.UnitId
                                 where p.Barcode.Equals(code)
                                 select new
                                 {
                                     p.ProductId,
                                     p.ProductName,
                                     p.Barcode,
                                     p.OnHand,
                                     p.Cost,
                                     pp.UnitId,
                                     pp.Price
                                 }).FirstOrDefaultAsync();
            return Ok(product);
        }
    }
}
