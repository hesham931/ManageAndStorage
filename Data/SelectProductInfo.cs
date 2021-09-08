using System;
using System.Globalization;
using ManageAndStorage.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ManageAndStorage.Data
{
    public class SelectProductInfo
    {
        public IList<Product> Select(IEnumerable<Product> objects, int id){
            IList<Product> obj = new List<Product>();
            foreach(var item in objects){
                if(item.ProductId == id){
                    obj.Add(item);
                }
            }

            return obj;
        }
        public int SelectId(IEnumerable<Product> obj){
            int result = 0;
            foreach(var item in obj){
                if(item.DisplayId != 0)
                    result = item.DisplayId;
            }
            return result;
        }
        public Item SelectItem(Product obj, int sales){
            Item result = new Item();

            result.Name = obj.Name;
            result.NumberOfItems = sales;
            result.ItemId = obj.id;
            result.Sum = sales*(obj.SalePrice*1.0);
            result.SaleDate = DateTime.Now;
            result.TempPage = 1;

            return result;
        }
        public Product UpdateItems(Product obj, int sales){
            if(obj.AvilableItems > 0){
                obj.AvilableItems -= sales;
            }
            

            return obj;
        }
        public Product UpdateProduct (Product obj, int plus){
            obj.AvilableItems += plus;

            return obj;
        }
    }
}