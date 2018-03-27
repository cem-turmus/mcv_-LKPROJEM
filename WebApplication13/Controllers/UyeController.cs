using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication13.Controllers
{
    using App_Class;
    using System.Web.Security;
    [Authorize]//erısımı engelledik giriş yapmadan ulasamasın.tum controler sayfalarında bu ıslemı yaptık ama bu gırıssayfasının bır ozellıgı var bız burda gırısyapacagız gırıs yapmadan buraya ulasamamak mantıksız olur o yuzden buraya ızın vermelıyız    [AllowAnonymous] ile.
    public class UyeController : Controller
    {
        // GET: GirişYap
        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Kullanici k, string hatirla)//kullanıcı k dıye bır sınıf acmıstık ıcınde username palola vardı
        {
            bool durum = Membership.ValidateUser(k.UserName, k.password);//bakıyoruz kullanıcı adı ve parola dogru mu dıye.
            if (durum)
            {
                if (hatirla == "on")
                    FormsAuthentication.RedirectFromLoginPage(k.UserName, true);//cokie de olustur
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(k.UserName, false);
                }//cokie de olusturma}
                return RedirectToAction("Index","Home");

            }
            else
            {
                ViewBag.mesaj = "Kullanıcı adı veya parola hatalı";
                return View();
            }

        }
        public  ActionResult CıkısYap ()
        {
            FormsAuthentication.SignOut();//cıkıs yapar
            return View("GirisYap");
        }
        [AllowAnonymous]
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SifremiUnuttum(Kullanici k)
        {
            MembershipUser user = Membership.GetUser(k.UserName);//kullanıcıyı aldık
            if (user.PasswordQuestion == k.passwordQuestion)//kullanıcının verdıgı gızlı cevap onceden olusturulan gızlı cevaba yanıverı tabanındakı gızlı cevaba esıtse
            {
                string pwd = user.ResetPassword(k.password);//gizli cevaba gore parolayı resetle dedık
                user.ChangePassword(pwd, k.password);//eski ve yenı parolaları yazdık ve degıstırdık.
                return RedirectToAction("GirisYap");
             
            }
            else
                ViewBag.mesaj = "Girilen bilgiler uyusmuyor";
            return RedirectToAction("SifremiUnuttum");
        }
    }
}