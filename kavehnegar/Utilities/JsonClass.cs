using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Helpers;
using System.Web.Mvc;

namespace kavehnegar.CacheStack
{

    public class JsonClass
    {
        // CacheStack _cacheStack =new CacheStack();
        // public JsonResult GetCountryList()
        // {
        //     if (_cacheStack.IsKeyExists("ExcelList"))
        //     {
        //         var List = _cacheStack.GetList<List<string>>("ExcelList");
        //         return Json(data: List, behavior: JsonRequestBehavior.AllowGet);
        //     }
        //     else
        //     {
        //         List<CountryMaster> countryList;
        //         using (var db = new DatabaseContext())
        //         {
        //             var countryListtemp = (from district in db.CountryMaster
        //                 select district).ToList();
        //
        //             _cacheStrigsStack.StoreList<List<CountryMaster>>("countryList", countryListtemp, TimeSpan.MaxValue);
        //             countryList = _cacheStrigsStack.GetList<List<CountryMaster>>("countryList");
        //         }
        //
        //         return Json(data: countryList, behavior: JsonRequestBehavior.AllowGet);
        //     }
        //
        // }
    }
}