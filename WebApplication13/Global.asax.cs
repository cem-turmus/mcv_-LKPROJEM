using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication13
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            if (Application["AktifKullaici"] == null)
            {
                int sayac = 1;
                Application["AktifKullanici"] = sayac;
                
            }
            else
            {
                int sayac = (int)Application["AktifKullaci"];
                sayac++;
                Application["AktifKullanici"] = sayac;
            }

            if (Application["ToplamKullanici"] == null)
            {
                int sayac = 1;
                Application["ToplamKullanici"] = sayac;

            }
            else
            {
                int sayac = (int)Application["ToplamKullanici"];
                sayac++;
                Application["ToplamKullanici"] = sayac;
            }
        }
        protected void Session_end()
        {

            int sayac = (int)Application["AktifKullaci"];
            sayac--;
            Application["AktifKullanici"] = sayac;
        }
    }
}
