using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
    public class SatisController : Controller
    {
		// GET: Satis
		Context context = new Context();
		public ActionResult Index()
        {
			var satislar = context.SatisHarakets.ToList();
			return View(satislar);
		}

		[HttpGet]
		public ActionResult YeniSatis()
		{
			List<SelectListItem> urunler = (from x in context.Uruns.ToList()
											select new SelectListItem
											{
												Text = x.UrunAd,
												Value = x.Urunid.ToString()
											}).ToList();
			List<SelectListItem> cariler = (from x in context.Carilers.ToList()
											select new SelectListItem
											{
												Text = x.CariAd + " " + x.CariSoyad,
												Value = x.CariID.ToString()
											}).ToList();
			List<SelectListItem> personeller = (from x in context.Personels.ToList()
												select new SelectListItem
												{
													Text = x.PersonelAd + " " + x.PersonelSoyad,
													Value = x.PersonelID.ToString()
												}).ToList();
			ViewBag.urun = urunler;
			ViewBag.cari = cariler;
			ViewBag.personel = personeller;

			return View();
		}
		[HttpPost]
		public ActionResult YeniSatis(SatisHareket s)
		{
			s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
			context.SatisHarakets.Add(s);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult SatisGetir(int id)
		{
			var satis = context.SatisHarakets.Find(id);

			List<SelectListItem> urunler = (from x in context.Uruns.ToList()
											select new SelectListItem
											{
												Text = x.UrunAd,
												Value = x.Urunid.ToString()
											}).ToList();
			List<SelectListItem> cariler = (from x in context.Carilers.ToList()
											select new SelectListItem
											{
												Text = x.CariAd + " " + x.CariSoyad,
												Value = x.CariID.ToString()
											}).ToList();
			List<SelectListItem> personeller = (from x in context.Personels.ToList()
												select new SelectListItem
												{
													Text = x.PersonelAd + " " + x.PersonelSoyad,
													Value = x.PersonelID.ToString()
												}).ToList();
			ViewBag.urun = urunler;
			ViewBag.cari = cariler;
			ViewBag.personel = personeller;

			return View("SatisGetir", satis);
		}

		public ActionResult SatisGuncelle(SatisHareket s)
		{
			var sts = context.SatisHarakets.Find(s.SatisID);

			sts.CariID = s.CariID;
			sts.PersonelID = s.PersonelID;
			sts.Urunid = s.Urunid;
			sts.Adet = s.Adet;
			sts.Fiyat = s.Fiyat;
			sts.Tarih = s.Tarih;
			sts.ToplamTutar = s.ToplamTutar;

			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult SatisDetay(int id)
		{
			var satis = context.SatisHarakets.Where(x => x.SatisID == id).ToList();
			return View(satis);
		}
	}
}