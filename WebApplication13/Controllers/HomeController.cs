using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication13.Controllers
{
    using Models;
    using App_Class;
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CookieAta()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CookieAta(string CookiName, string CookieValue)
        {
            HttpCookie ck = new HttpCookie(CookiName);
            ck.Value = CookieValue;//bı ust satırda new dedıgımızdede metot ıcıne de verebılırdık
            ck.Expires = DateTime.Now.AddDays(1);//cookie browserda ne kadar tutulacak.Biz bir gun ekledık.Bilgilerimizi cerezlerden 1 gun boyunca alıyor 1 gun sonra sılıyor.
            Response.Cookies.Add(ck);//ck yi browsera cokie eklıyor response aracılıgıya.
            return View();
        }
        public ActionResult Sepetim()
        {
            List<Products> urunler = new List<Products>();//sepete gıdecek urunler ıcın lıst olusturduk
            if (Session["AktifSepet"] != null)//sepet bos değilse dedık
            {
                Sepet s = (Sepet)Session["AktifSepet"];//sepeti aldık sesıondan
                urunler = s.Urunler;//septtekı urunlerı gonderılecek ,olustumus oldugumuz urunler lısetesıne ekledıkı.
            }
            return View(urunler);
        }
        public ActionResult kullanicisayisi()
        {
            ViewBag.AktifKullanici = HttpContext.Application["AktifKullanici"];
            ViewBag.ToplamKullanici = HttpContext.Application["ToplamKullanici"];
            return View();
        }
    }
}
