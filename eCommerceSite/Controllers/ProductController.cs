using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            //Get all products from database
            List<Product> products = await _context.products.ToListAsync();

            //Send list of products to view to be displayed
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                //Add to DB
                _context.products.Add(product);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{product.Title} was added successfully";
                //Redirect back to catalog page
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
