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
            //this.products = products;
        }
        
        public string categoryName { get; set; }
        public string totalProducts { get; set; }
        public int id { get; set; }
        //public string products { get; set; }
    }

    public class Product
    {
        public Product(int id, string product)
        {
            this.id = id;
            this.product = product;
        }

        public string product { get; set; }
        public int id { get; set; }
    }
}
