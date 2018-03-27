using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication13.Controllers
{
    public class TedarikcilerController : Controller
    {
        Models.Nortwind ctx = new Models.Nortwind();
        [Authorize]
        // GET: Tedarikciler
        public ActionResult Index()
        {
         List<Models.Suppliers>tedarıkcı= ctx.Suppliers.ToList();
            return View(tedarıkcı);
        }
    }
}