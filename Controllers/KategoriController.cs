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
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context context = new Context();
        public ActionResult Index(int sayfa = 1)
        {
			var kategoriler = context.Kategoris.ToList().ToPagedList(sayfa, 5);
			return View(kategoriler);
        }

		[HttpGet]
		public ActionResult KategoriEkle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult KategoriEkle(Kategori k)
		{
			context.Kategoris.Add(k);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult KategoriSil(int id)
		{
			var ktgr = context.Kategoris.Find(id);
			context.Kategoris.Remove(ktgr);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult KategoriGetir(int id)
		{

			var kategori = context.Kategoris.Find(id);
			return View("KategoriGetir", kategori);
		}

		public ActionResult KategoriGuncelle(Kategori k)
		{
			var ktgr = context.Kategoris.Find(k.KategoriID);
			ktgr.KategoriAd = k.KategoriAd;
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Deneme()
		{
			Class3 cs = new Class3();
			cs.Kategoriler = new SelectList(context.Kategoris, "KategoriID", "KategoriAd");
			cs.Urunler = new SelectList(context.Uruns, "Urunid", "UrunAd");
			return View(cs);
		}

		public JsonResult UrunGetir(int p)
		{
			var urunlistesi = (from x in context.Uruns
							   join y in context.Kategoris
							   on x.Kategori.KategoriID equals y.KategoriID
							   where x.Kategori.KategoriID == p
							   select new
							   {
								   Text = x.UrunAd,
								   Value = x.Urunid.ToString()
							   }).ToList();
			return Json(urunlistesi, JsonRequestBehavior.AllowGet);
		}
	}
}