using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Product format of program
    /// UI interacts with this format of the Product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Name of the Product
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Price of the Product
        /// </summary>
        public decimal Price { get; set; }
        public int ProdID { get; set; }
    }
}
