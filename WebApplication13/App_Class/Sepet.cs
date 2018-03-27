using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication13.App_Class
{
    using Models;
    public class Sepet
    {
        private List<Products> urunler=new List<Products>();
        //kullanılmadan once new lensın .New lenmezse ram de uretılmeyen bısey ıcın hata verecek.Var olan sepet ıcın newlemez yenı sepet olusturyorsak gelır bu sınıfa sepetı newler.
        public List<Products> Urunler
        {
            get { return urunler; }
            set { urunler = value; }
        }

    }
}