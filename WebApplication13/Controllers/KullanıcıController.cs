using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication13.App_Class;

namespace WebApplication13.Controllers
{[Authorize]
    public class KullanıcıController : Controller
    {
        Models.Nortwind ctx = new Models.Nortwind();
        // GET: Kullanıcı
        public ActionResult Index()
        {
            MembershipUserCollection users=  Membership.GetAllUsers();//Verıtabanındakı kullanıcıları aldık
            return View(users);
        }
        [AllowAnonymous]//kullanıcı basta uye değilse kayıt olacak burdan o yuzden buraya gırmesıne ızın vermelıyız ondan dolayı allowanony dedık.
        public ActionResult Ekle()
        {
            return View();
        }
        [AllowAnonymous]//kullanıcı basta uye değilse kayıt olacak burdan o yuzden buraya gırmesıne ızın vermelıyız ondan dolayı allowanony dedık.
        [HttpPost]
        public ActionResult Ekle(Kullanici k)//kullanıcı sınıfını neden olusturduk?cevap:bız bu metoda tejk tek yazmamak ıcın ekle me ıcın sınıfı gonderıryorduk ama bızım eklemeye calıstgımız kullanıcı ıcın gızlı soru gızlı cevap vs ıcın bır sıınıf yok o yuzden sınıf yaratıp sınıfı gonderdık. Membership.CreateUser METODUNUN ICINDEKI TRUE aktıf bı kullanıcı mı dıyor ,membershıpstate ıse kullanıcı basarılı olsuturuldu veya hata sebebını gerı dondurur.

        {
            MembershipCreateStatus durum;
            Membership.CreateUser(k.UserName, k.password, k.Email, k.passwordQuestion, k.passwordAnswer, true, out durum);
            string mesaj = "";
            switch (durum)
            {
            //asagıdakılerını bız yazmadık enumarıton getırdı durum yazıp entere bastıgımızda.

                case MembershipCreateStatus.Success:

                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "Gecersiz kullanıcı adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "Gecersiz parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "Gecersiz gızlı soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "Gecersiz gizlicevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "Gecersiz email";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Kullanılmış kullanıcı hatası";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Kullanılmış Email  girildi";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += "Kullanıcı engel hatası";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += "Gcersiz kullanıcı key";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Kullanılmış kullanıcı key hatası";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += "";
                    break;
                default:
                    break;
            }
            ViewBag.mesaj = mesaj;
            if (durum == MembershipCreateStatus.Success)//durum success ıse
                return RedirectToAction("Index");
            else
                return View();


        }
        [Authorize(Roles = "Admin")]
        public ActionResult RolAta(string id)//butona tıklayınca ıd  yı aldık .Yani biz kullanıcılar ındex sayfasındakı    <td><a href="/Kullanıcı/RolAta/@MU.UserName"class="btn-success"data-Uyeadi="@MU.UserName">Rol Ata</a></td> butonuna tıklayınca lınkıne ek olrak mu.username de verdık ve bu mu.usernam rolatanın actıon a gelıp id olarak geldi.ve rolatama sayfasına labela da , <input type="text" name="UserName" value="@ViewBag.kullaniciAdi" disabled="disabled" class="form-control" id="exampleInputEmail1"> ,sayesınde yazıldı.
        {
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            ViewBag.kullaniciAdi = ctx.aspnet_Users.FirstOrDefault(x => x.UserName == id).UserName.ToString();//burada id ye gore rolatancak kısıyı bulup vıevbag sayesıonde dıger sayfaya gonderdık.
            return View();

        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult RolAta(string UserName, string Role)
        {
            Roles.AddUserToRole(UserName, Role);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult UyeRolleri(string UserName)
        {
            List<string> Roller = Roles.GetRolesForUser(UserName).ToList();
            string rol="";
            foreach (string r in Roller)
            {
                rol +=" - "+ r;
            }
            return Json(rol);
        }
    }
}
