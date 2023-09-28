using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari

        Context context = new Context();
        public ActionResult Index()
        {
			var cariler = context.Carilers.Where(x => x.Durum == true).ToList();
			return View(cariler);
        }

		[HttpGet]
		public ActionResult CariEkle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult CariEkle(Cariler p)
		{
			p.Durum = true;
			context.Carilers.Add(p);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult CariSil(int id)
		{
			var cari = context.Carilers.Find(id);
			cari.Durum = false;
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult CariGetir(int id)
		{
			var cari = context.Carilers.Find(id);
			return View("CariGetir", cari);
		}

		public ActionResult CariGuncelle(Cariler p)
		{
			if(!ModelState.IsValid)
			{
				return View("CariGetir");
			}
			var cari = context.Carilers.Find(p.CariID);
			cari.CariAd = p.CariAd;
			cari.CariSoyad = p.CariSoyad;
			cari.CariSehir = p.CariSehir;
			cari.CariMail = p.CariMail;

			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult SatisGecmisi(int id)
		{
			var satislar = context.SatisHarakets.Where(x => x.CariID == id).ToList();
			var cr = context.Carilers.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
			ViewBag.cari = cr;

			return View(satislar);
		}
	}
}