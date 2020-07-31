using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a View list all products.
        /// </summary>
        public IActionResult Index()
        {
            //Get all products from database
            List<Product> products = _context.products.ToList();

            //Send list of products to view to be displayed
            return View(products);
        }
    }
}
