using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        /// Displays a View list a page of products.
        /// </summary>
        public async Task<IActionResult> Index(int? id)
        {
            int pageNum = id ?? 1;
            const int pageSize = 3;
            //int pageNum = id.HasValue ? id.Value : 1; Ternary
            ViewData["CurrentPage"] = pageNum;
            int numProducts = await ProductDB.GetTotalProductsAsync(_context);

            // 10 Products
            // 3 per page
            int totalPages = (int)Math.Ceiling((double)numProducts / pageSize);
            ViewData["MaxPage"] = totalPages;
            //Get all products from database
            //List<Product> products = await _context.products.ToListAsync();
            List<Product> products = await ProductDB.GetProductsAsync(_context, pageSize, pageNum);

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
                await ProductDB.AddProductAsync(_context, product);
                TempData["Message"] = $"{product.Title} was added successfully";
                //Redirect back to catalog page
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Get product with corresponding id
            //Product p =
            //    await (from prod in _context.products
            //    where prod.ProductId == id
            //    select prod).SingleAsync();

            //Product p =
            //    await _context.products
            //        .Where(prod => prod.ProductId == id).SingleAsync();

            Product p = await ProductDB.GetProductAsync(_context, id);

            //Pass product to view
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                ViewData["Message"] = "Product updated successfully";
            }
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product p = await ProductDB.GetProductAsync(_context, id);

            return View(p);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirtmed(int id)
        {
            Product p = await ProductDB.GetProductAsync(_context, id);

            _context.Entry(p).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            TempData["Message"] = $"{p.Title} was deleted";
            return RedirectToAction("Index");
        }
    }
}
