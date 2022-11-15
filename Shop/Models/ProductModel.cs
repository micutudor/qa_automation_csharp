using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    class ProductModel
    {
        public int Id;
        public string Name;

        private int _price;

        public string Price
        {
            get => this._price.ToString();

            set => this._price = int.Parse(value.TrimStart('$'));
        }

        public ProductModel(int id, string name, string price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public bool Equals(ProductModel product) => Name == product.Name && Price == product.Price;
   
    }
}
