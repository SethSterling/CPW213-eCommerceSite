using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
    /// <summary>
    /// A salable Product
    /// </summary>
    public class Product
    {
        [Key]// Make Primary key in db
        public int ProductId { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The price of the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The category the product falls under.
        /// </summary>
        public string Category { get; set; }
    }
}
