using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication13.Content
{
    public class TedarikciController : Controller
    {
        Models.Nortwind ctx = new Models.Nortwind();
        // GET: Tedarikci
        public ActionResult Index()
        {
            List<Models.Suppliers> tedarikciler = ctx.Suppliers.ToList();
            return View(tedarikciler);
        }
        public ActionResult Ekle()
        {
            return View();
        }
    }
}