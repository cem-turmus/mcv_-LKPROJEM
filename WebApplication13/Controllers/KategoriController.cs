using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication13.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        Models.Nortwind ctx = new Models.Nortwind();
        // GET: Kategoriler
        public ActionResult Index()
        {
        List<Models.Categories> ktgler = ctx.Categories.ToList();
            return View(ktgler);
        }
        public ActionResult Ekle()//sadece goruntuleme yapıyor ekleme metodu için.
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Models.Categories c)
        {
            ctx.Categories.Add(c);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Sil(int id)
        {
            //  Normalde get olan actıon da urunu alırdık burda ıse post ectıon ıcınde hem aldık hem de sıldık
            Models.Categories c = ctx.Categories.FirstOrDefault(x => x.CategoryID == id);//id li urunu aldık
            ctx.Categories.Remove(c);//sildik
            ctx.SaveChanges();//değişiklikleri kaydettik.
            //ActionResult yerıne void neden yaptık:Eger silme anında bır hata alırsak safya yenılme yapmasın dıye .Zaten ajax yenılemeyı yapıyordu(yanı yapması ıcın gereken kodaları yazdık)

            return Json(new { hasError = false, message= "Kayıt basarıyla silindi" });
        }
    }
}
