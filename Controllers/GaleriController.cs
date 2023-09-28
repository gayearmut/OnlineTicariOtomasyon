using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
    public class GaleriController : Controller
    {
		// GET: Galeri
		Context context = new Context();
		public ActionResult Index()
        {
			var urunler = context.Uruns.ToList();
			return View(urunler);
        }
    }
}