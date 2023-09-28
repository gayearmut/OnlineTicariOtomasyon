using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context context = new Context();
        public ActionResult Index()
        {
			var faturalar = context.Faturas.ToList();
			return View(faturalar);
        }
		[HttpGet]
		public ActionResult FaturaEkle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult FaturaEkle(Fatura f)
		{
			context.Faturas.Add(f);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult FaturaGetir(int id)
		{
			var fatura = context.Faturas.Find(id);
			return View("FaturaGetir", fatura);
		}
		public ActionResult FaturaGuncelle(Fatura f)
		{
			var fatura = context.Faturas.Find(f.FaturaID);

			fatura.FaturaSeriNo = f.FaturaSeriNo;
			fatura.FaturaSiraNo = f.FaturaSiraNo;
			fatura.Tarih = f.Tarih;
			fatura.VergiDairesi = f.VergiDairesi;
			fatura.Saat = f.Saat;
			fatura.TeslimAlan = f.TeslimAlan;
			fatura.TeslimEden = f.TeslimEden;
			fatura.Toplam = f.Toplam;
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult FaturaDetay(int id)
		{
			var kalemler = context.FaturaKalems.Where(x => x.FaturaID == id).ToList();

			return View(kalemler);
		}

		[HttpGet]
		public ActionResult YeniKalem()
		{
			return View();
		}
		public ActionResult YeniKalem(FaturaKalem p)
		{
			context.FaturaKalems.Add(p);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Dinamik()
		{
			Class4 cs = new Class4();
			cs.deger1 = context.Faturas.ToList();
			cs.deger2 = context.FaturaKalems.ToList();
			return View(cs);
		}


		public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo, DateTime Tarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
		{
			Fatura f = new Fatura();
			f.FaturaSeriNo = FaturaSeriNo;
			f.FaturaSiraNo = FaturaSiraNo;
			f.Tarih = Tarih;
			f.VergiDairesi = VergiDairesi;
			f.Saat = Saat;
			f.TeslimEden = TeslimEden;
			f.TeslimAlan = TeslimAlan;
			f.Toplam = decimal.Parse(Toplam);
			context.Faturas.Add(f);
			foreach (var x in kalemler)
			{
				FaturaKalem fk = new FaturaKalem();
				fk.Aciklama = x.Aciklama;
				fk.BirimFiyat = x.BirimFiyat;
				fk.FaturaID = x.FaturaKalemID;
				fk.Miktar = x.Miktar;
				fk.Tutar = x.Tutar;
				context.FaturaKalems.Add(fk);
			}
			context.SaveChanges();
			return Json("İşlem Başarılı");
		}
	}
}