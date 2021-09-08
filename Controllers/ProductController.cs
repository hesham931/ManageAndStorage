using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ManageAndStorage.Data;
using ManageAndStorage.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace ManageAndStorage.Controllers
{
    //[Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext _Db;
        public SelectProductInfo ProductInfo;
        private readonly Search _S;

        public ProductController(ILogger<ProductController> logger, ApplicationDbContext Db, SelectProductInfo info, Search S)
        {
            _logger = logger;
            _Db = Db;
            ProductInfo = info;
            _S = S;
        }

        public IActionResult Index(int id)
        {
            IEnumerable<Product> obj = _Db.Products;
            IList<Product> obj1 = ProductInfo.Select(obj, id);

            if(obj1.Count() == 0){
                return RedirectToAction("Create", new{ProductId = id});
            }
            return View(obj1);
        }
        public IActionResult DisplayInforamtion(int id){
            IEnumerable<Product> obj = _Db.Products;
            IList<Product> obj1 = ProductInfo.Select(obj, id);

            return View("Index", obj1);
        }
        public IActionResult Create(int ProductId){
            Product obj = new Product();
            obj.ProductId = ProductId;
           
            return View(obj);
        }
        [HttpPost]
        public RedirectToActionResult Create(Product obj){
            IList<Product> obj1 = ProductInfo.Select(_Db.Products, obj.ProductId);
            int SelectId = ProductInfo.SelectId(obj1);
            obj.DisplayId = SelectId + 1;
            
            _Db.Products.Add(obj);
            _Db.SaveChanges();

            return RedirectToAction("index", new {id = obj.ProductId});
        }
        public IActionResult Edit(int id){
            var obj = _Db.Products.Find(id);

            return View(obj);
        }
        [HttpPost]
        public RedirectToActionResult Edit(Product obj){
            _Db.Products.Update(obj);
            _Db.SaveChanges();

            return RedirectToAction("DisplayInforamtion", new{id = obj.ProductId});
        }
        [Route("Product/Search")]
        [HttpPost]
        public IActionResult SearchName(int ProductId, string Pattren){
            IEnumerable<Product> obj = _S.SearchName(_Db, Pattren, ProductId);


            return View("search", obj);
        }
        //[Route("Product/Search")]
        [HttpPost]
        public IActionResult SearchId(int ProductId, int Pattren){
            IEnumerable<Product> obj = _S.SearchId(_Db, Pattren, ProductId);
            return View("search", obj);
        }
        //[Route("Product/Search")]
        [HttpPost]
        public IActionResult SearchItemLocation(int ProductId, int Pattren){
            IEnumerable<Product> obj = _S.SearchItemLocation(_Db, Pattren, ProductId);
            return View("search", obj);
        }
        public IActionResult DisplayEmptyItems(int ProductId){
            IEnumerable<Product> obj = _S.DisplayEmptyItems(_Db, ProductId);

            return View("EmptyList", obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}