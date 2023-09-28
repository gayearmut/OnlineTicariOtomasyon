using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
	public class KargoController : Controller
    {
		Context context = new Context();
		// GET: Kargo
		public ActionResult Index(string p)
        {
			var k = from x in context.KargoDetays select x;
			if (!string.IsNullOrEmpty(p))
			{
				k = k.Where(y => y.TakipKodu.Contains(p));
			}
			return View(k.ToList());
		}

		[HttpGet]
		public ActionResult YeniKargo()
		{
			string randomString = Guid.NewGuid().ToString().Substring(0, 10);
			ViewBag.trackingNumber = randomString;
			return View();
		}

		[HttpPost]
		public ActionResult YeniKargo(KargoDetay d)
		{
			context.KargoDetays.Add(d);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult KargoTakip(string id)
		{
			//p = "489A15B86D";
			var degerler = context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
			return View(degerler);
		}
	}
}