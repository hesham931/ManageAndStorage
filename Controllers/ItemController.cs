using System;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ManageAndStorage.Data;
using ManageAndStorage.Models;

namespace ManageAndStorage.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ApplicationDbContext _Db;
        private readonly SelectProductInfo _S;
        private readonly Search _search;

        public ItemController(ILogger<ItemController> logger, ApplicationDbContext Db, SelectProductInfo S, Search search)
        {
            _logger = logger;
            _Db = Db;
            _S = S;
            _search = search;
        }

        public IActionResult Index(){
            IEnumerable<Item> obj = _Db.Items;

            foreach(var Item in obj){
                Item.TempPage = 0;

                _Db.Items.Update(Item);
            }

            _Db.SaveChanges();
            return View(obj);
        }
        public IActionResult Display_Information(){
            IEnumerable<Item> obj = _Db.Items;

            return View("index", obj);
        }
        [HttpPost]
        public IActionResult SaleListWithId(int ItemId, int ProductId, int sales)
        {
            var obj = _Db.Products.Find(ItemId);

            if(obj.AvilableItems > 0){
                //this method will remove the sales items from the AvilableItems in database
                var UpdatedObject = _S.UpdateItems(obj, sales);
                _Db.Products.Update(UpdatedObject);

                var result = _S.SelectItem(obj, sales);
                _Db.Items.Add(result);
                _Db.SaveChanges();
            }
            return RedirectToAction("DisplayInforamtion", "Product", new {id = ProductId});
        }
        [HttpPost]
        public IActionResult UpdateListWithId(int Id, int ItemId, int NewSales){
            var obj = _Db.Products.Find(ItemId);
            var obj2 = _Db.Items.Find(Id);
            
            int test = obj.AvilableItems;
            obj.AvilableItems += obj2.NumberOfItems;
            if(NewSales <= obj.AvilableItems){
                obj.AvilableItems -= NewSales;
                obj2.NumberOfItems = NewSales;
                obj2.Sum = NewSales*(obj.SalePrice*1.0);
            }
            else{
                obj.AvilableItems = test;
            }

            _Db.Items.Update(obj2);
            _Db.SaveChanges();

            IEnumerable<Item> result = _Db.Items;
            return View("custom", result);
        }
        public IActionResult Custom(){
            IList<Item> result = new List<Item>();
            
            foreach(var Item in _Db.Items){
                if(Item.TempPage == 1) result.Add(Item);
            }
            
            return View(result);
        }
        public RedirectToActionResult DeleteItem(int id, int NumberOfItems){
            var obj = _Db.Items.Find(id);
            var obj2 = _Db.Products.Find(obj.ItemId);
            var obj3 = _S.UpdateProduct(obj2, NumberOfItems);

            _Db.Products.Update(obj3);
            _Db.Items.Remove(obj);
            _Db.SaveChanges();

            return RedirectToAction("Custom");
        }
        [Route("Item/Search")]
        [HttpPost]
        public IActionResult SearchName(string Pattren){
            IEnumerable<Item> obj = _search.SearchNameInSales(_Db, Pattren);


            return View("search", obj);
        }
        [HttpPost]
        public IActionResult SearchId(int Pattren){
            IEnumerable<Item> obj = _search.SearchIdInSales(_Db, Pattren);
            return View("search", obj);
        }
        [Route("Item/filter_option/Apply_filtring")]
        [HttpPost]
        public IActionResult Fitering(string FilterOption){
            IEnumerable<Item> obj = _search.Filtring(_Db, FilterOption);

            return View("Fitering", obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}