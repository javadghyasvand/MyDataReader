using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kavehnegar.CacheStack;
using kavehnegar.Models;
using OfficeOpenXml;

namespace kavehnegar.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDataBaseEntities _db = new MyDataBaseEntities();
        private readonly CacheStack.CacheStack _cache = new CacheStack.CacheStack();
        public ActionResult SetToRedis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetToRedis(HttpPostedFileBase fileLoad)
        {
            if (fileLoad != null)
            {
                if (Path.GetExtension(fileLoad.FileName) == ".xlsx" || Path.GetExtension(fileLoad.FileName) == ".xls")
                {
                    string[] listString;
                    ExcelPackage package = new ExcelPackage(fileLoad.InputStream);
                    var listItem = package.ToStringList();
                    _cache.StoreList("ExcelList", listItem, TimeSpan.MaxValue);
                    return RedirectToAction("Index");
                }
                else
                {
                   ViewBag.Error= "لطفا فایل اکسل وارد کنید";
                }
            }
            else
            {
                ViewBag.Error = "فایل ی فایل انتخاب کنید";
               
            }
            return View("SetToRedis");
        }

        // GET: Home
        public ActionResult Index()
        {
            if (_cache.IsKeyExists("ExcelList"))
            {
                MyTBLViewModel model = new MyTBLViewModel();
                List<MyTBLViewModel> list = new List<MyTBLViewModel>();
                foreach (var item in _cache.GetList<List<string>>("ExcelList"))
                {
                    var viewModel = new MyTBLViewModel()
                    {
                        DataString = item
                    };

                    list.Add(viewModel);
                }

                return View(list);
            }
            return View();
        }
        public ActionResult ListData()
        {
            return View(_db.MyTBL.ToList());
        }

        // GET: Home/Create
        public ActionResult Create(string id)
        {
            return PartialView(new MyTBL()
            {
                DataString = id.ToString(),
            });
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DataString")] MyTBL myTBL)
        {
            if (myTBL.DataString !=null)
            {
                _db.MyTBL.Add(myTBL);
                _db.SaveChanges();
              
            }
            return RedirectToAction("Index");

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
