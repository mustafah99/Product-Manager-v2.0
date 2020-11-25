using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Manager_v2._0
{
    public class Categories
    {
        public Categories(int id, string categoryName, string totalProducts)
        {
            this.id = id;
            this.categoryName = categoryName;
            this.totalProducts = totalProducts;
        }
        
        public string categoryName { get; set; }
        public string totalProducts { get; set; }
        public int id { get; set; }
    }
}
