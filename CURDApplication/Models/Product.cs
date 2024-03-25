using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CURDApplication.Models
{
    public class Product
    {
        public int Pro_ID { get; set; }
        [DisplayName("Product Name")]
        public string Pro_Name { get; set; }
        [DisplayName("Product Price")]
        public string Pro_Price { get; set; }
        [DisplayName("Product Count")]
        public string Pro_Count { get; set; }
    }
}