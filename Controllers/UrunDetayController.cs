using OnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Ticari_Otomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
		// GET: UrunDetay
		Context context = new Context();
		public ActionResult Index()
        {
			Class1 cs = new Class1();
			// var degerler = c.Uruns.Where(x => x.Urunid == 1).ToList();
			cs.Deger1 = context.Uruns.Where(x => x.Urunid == 1).ToList();
			cs.Deger2 = context.Detays.Where(y => y.DetayID == 1).ToList();
			return View(cs);
		}
    }
}