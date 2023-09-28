using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
	public class UrunController : Controller
	{

		Context context = new Context();
		public ActionResult Index(string p, int sayfa = 1)
		{
			//var urunler = context.Uruns.Where(x =>x.Durum == true).ToList().ToPagedList(sayfa, 5);
			//return View(urunler);
			var degerler = context.Uruns.Where(x => x.UrunAd.Contains(p) || p == null).ToList().ToPagedList(sayfa, 5);
			return View(degerler);
		}
		[HttpGet]
		public ActionResult YeniUrun()
		{
			List<SelectListItem> kategoriler = (from x in context.Kategoris.ToList()
												select new SelectListItem
												{
													Text = x.KategoriAd,
													Value = x.KategoriID.ToString()
												}).ToList();
			ViewBag.ktgr = kategoriler;
			return View();
		}
		[HttpPost]
		public ActionResult YeniUrun(Urun p)
		{
			context.Uruns.Add(p);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult UrunSil(int id)
		{
			var urn = context.Uruns.Find(id);
			urn.Durum = false;
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult UrunGetir(int id)
		{
			List<SelectListItem> kategoriler = (from x in context.Kategoris.ToList()
												select new SelectListItem
												{
													Text = x.KategoriAd,
													Value = x.KategoriID.ToString()
												}).ToList();
			ViewBag.ktgr = kategoriler;

			var urundeger = context.Uruns.Find(id);
			return View("UrunGetir", urundeger);
		}

		public ActionResult UrunGuncelle(Urun p)
		{
			var urn = context.Uruns.Find(p.Urunid);

			urn.UrunAd = p.UrunAd;
			urn.Marka = p.Marka;
			urn.Stok = p.Stok;
			urn.AlisFiyat = p.AlisFiyat;
			urn.SatisFiyat = p.SatisFiyat;
			urn.Durum = p.Durum;
			urn.UrunGorsel = p.UrunGorsel;
			urn.KategoriID = p.KategoriID;

			context.SaveChanges();
			return RedirectToAction("Index");

		}
		public ActionResult UrunListesi()
		{
			var urunler = context.Uruns.ToList();
			return View(urunler);
		}
		[HttpGet]
		public ActionResult SatisYap(int id)
		{
			List<SelectListItem> deger3 = (from x in context.Personels.ToList()
										   select new SelectListItem
										   {
											   Text = x.PersonelAd + " " + x.PersonelSoyad,
											   Value = x.PersonelID.ToString()
										   }).ToList();

			ViewBag.dgr3 = deger3;
			var deger1 = context.Uruns.Find(id);
			ViewBag.dgr1 = deger1.Urunid;
			ViewBag.dgr2 = deger1.SatisFiyat;
			return View();
		}
		[HttpPost]
		public ActionResult SatisYap(SatisHareket p)
		{
			p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
			context.SatisHarakets.Add(p);
			context.SaveChanges();
			return RedirectToAction("Index", "Satis");
		}


	}
}