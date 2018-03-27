using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication13.Controllers
{
    using App_Class;
    [Authorize]
    public class UrunController : Controller
    {
        Models.Nortwind ctx = new Models.Nortwind();
        // GET: Urun
        public ActionResult Index()
        {
            List<Models.Products> urunler = ctx.Products.ToList();

            return View(urunler);
        }
        [AllowAnonymous]
        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = ctx.Categories.ToList();
            ViewBag.Tedarikciler = ctx.Suppliers.ToList();
            return View();
        }
        [AllowAnonymous]//kullanıcı basta uye değilse kayıt olacak burdan o yuzden buraya gırmesıne ızın vermelıyız ondan dolayı allowanony dedık.
        [HttpPost]
        public ActionResult UrunEkle(Models.Products p)//50 tane parametre olsa veya daha fazla bunu elımızlemı verecegız hayır.Pekı ne yapacagız.Gidip html sayfasından inputlara sınıfımızın propertylerın yazacagız(aynı olacak harf hatası olmaycak).Buraya gelıp product p veya sınıf s gıbı ınstance aldıktan sonra gelen ınstance .ekleme anında ınputlara gırılen degerı alıp gelecek
        {
            ctx.Products.Add(p);//ekledı
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            Models.Products p = ctx.Products.FirstOrDefault(x => x.ProductID == id);//buraya id ındex.cshtml den gelcek.
            return View(p);//secılen urunu de model yontemıyle sıl vıeweıne gonderıyoruz.
        }
        [HttpPost]//urun sılme işlemını yapıyoruz şimdi
        public ActionResult Sil(Models.Products p)//yukarda id gonderdık burdada ıd gonderemeyız sımdı p olarak ınstance gonderdık
        {
            Models.Products urn = ctx.Products.FirstOrDefault(x =>x.ProductID==p.ProductID);
            ctx.Products.Remove(urn);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        //urundetay yontemını QueryString ile yaptık.Adres kısmında su ıd ve su adlı urun olarak adresı verdık./urun/urunler/urun.urunıd den farkı burda ıd dısında ısımını de gosterebılıyoruz.
       // reguest gonderme ıslemıdır web sayfasına,response ıse web sayfasından alma ıslemıdır.
        public ActionResult UrunDetay()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string adi = Request.QueryString["ProductNAME"];
           Models.Products p = ctx.Products.FirstOrDefault(x => x.ProductID == id);
            return View(p);
        }
        [HttpPost]
        public void SepeteAt(int id)
        {
            Sepet s;
            if (Session["AktifSepet"] == null)//eger sesionda bısey yoksa sepet yok demktır yenı sepet uretıyoruz.
            {
                s= new Sepet();
           
              
            }
            else
            {
                s =(Sepet) Session["AktifSepet"];  //session[SepeteAt] obje dir .bız ona bu bır sepettır dedık .sepet bo değilse sesıon ozellıgıyle elımızdekı sepeti alıyoruz yani bi sepet daha new leyıp yaratmıyoruz.
            }
            Models.Products p = ctx.Products.FirstOrDefault(x => x.ProductID == id);
            s.Urunler.Add(p);
            Session["AktifSepet"] = s;//sesionda sepetı tutuyoruz tekrar
           
        }
    }
}