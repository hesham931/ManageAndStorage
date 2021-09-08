using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ManageAndStorage.Models;
using ManageAndStorage.Data;

namespace ManageAndStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _Db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext Db)
        {
            _logger = logger;
            _Db = Db;
        }

        public IActionResult Index()
        {
            IEnumerable<BussinessType> obj = _Db.BussinessTypes;
            return View(obj);
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public RedirectToActionResult Create(BussinessType obj){
            _Db.BussinessTypes.Add(obj);
            _Db.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Edit(int? id){
            var obj = _Db.BussinessTypes.Find(id);
            return View(obj);
        }
        [HttpPost]
        public RedirectToActionResult Edit(BussinessType obj){
            _Db.BussinessTypes.Update(obj);
            _Db.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int? id){
            var obj = _Db.BussinessTypes.Find(id);

            return View(obj);
        }
        [HttpPost]
        public RedirectToActionResult Delete(BussinessType obj){
            _Db.BussinessTypes.Remove(obj);
            _Db.SaveChanges();

            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
