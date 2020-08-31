using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
    public static class ProductDB
    {
        /// <summary>
        /// Returns the total count of products
        /// </summary>
        /// <param name="_context">Database context to use</param>
        public async static Task<int> GetTotalProductsAsync(ProductContext _context)
        {
            return await (from p in _context.products
                   select p).CountAsync();
        }

        /// <summary>
        /// Get a page worth of products
        /// </summary>
        /// <param name="_context">Database context to use</param>
        /// <param name="pageSize">The number of products per page</param>
        /// <param name="pageNum">Page of products to return</param>
        /// <returns></returns>
        public static async Task<List<Product>> GetProductsAsync(ProductContext _context, int pageSize, int pageNum)
        {
           return await (from p in _context.products
                   orderby p.Title ascending
                   select p)
                   .Skip(pageSize * (pageNum - 1))
                   .Take(pageSize)
                   .ToListAsync();
        }

        public static async Task<Product> AddProductAsync(ProductContext _context, Product product)
        {
            //Add to DB
            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public static async Task<Product> GetProductAsync(ProductContext _context, int id)
        {
            Product p = await (from products in _context.products
                               where products.ProductId == id
                               select products).SingleAsync();
            return p;
        }
    }
}
