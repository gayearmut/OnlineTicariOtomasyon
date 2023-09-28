using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
	public class IstatistikController : Controller
	{
		// GET: Istatistik
		Context context = new Context();
		public ActionResult Index()
		{
			var deger1 = context.Carilers.Count().ToString();
			ViewBag.d1 = deger1;

			var deger2 = context.Uruns.Count().ToString();
			ViewBag.d2 = deger2;

			var deger3 = context.Personels.Count().ToString();
			ViewBag.d3 = deger3;

			var deger4 = context.Kategoris.Count().ToString();
			ViewBag.d4 = deger4;

			var deger5 = context.Uruns.Sum(x => x.Stok).ToString();
			ViewBag.d5 = deger5;

			var deger6 = (from x in context.Uruns select x.Marka).Distinct().Count().ToString();
			ViewBag.d6 = deger6;

			var deger7 = context.Uruns.Count(x => x.Stok <= 20).ToString();
			ViewBag.d7 = deger7;

			//var deger8 = (from x in context.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
			//var deger8 = context.Uruns.OrderByDescending(x=>x.SatisFiyat).Select(x => x.UrunAd).FirstOrDefault();
			var enYuksekFiyat = context.Uruns.Max(x => x.SatisFiyat);
			var deger8 = context.Uruns.FirstOrDefault(x => x.SatisFiyat == enYuksekFiyat)?.UrunAd;
			ViewBag.d8 = deger8;

			var deger9 = (from x in context.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
			ViewBag.d9 = deger9;

			var deger10 = context.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
			ViewBag.d10 = deger10;

			var deger11 = context.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
			ViewBag.d11 = deger11;

			var deger12 = context.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
			ViewBag.d12 = deger12;

			var deger13 = context.Uruns.Where(u => u.Urunid == (context.SatisHarakets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
			ViewBag.d13 = deger13;

			var deger14 = context.SatisHarakets.Sum(x => x.ToplamTutar).ToString();
			ViewBag.d14 = deger14;

			DateTime bugun = DateTime.Now.Date;//bugün yapılan satışlar
			var deger15 = context.SatisHarakets.Count(x => x.Tarih == bugun).ToString();
			ViewBag.d15 = deger15;

			var deger16 = context.SatisHarakets.Where(x => x.Tarih == bugun).Sum(y => (decimal?)y.ToplamTutar).ToString();//bugün yapılan satışların toplam tutarı
			ViewBag.d16 = deger16;
			if (deger15 == "0")
			{
				deger16 = "0";
			}
			ViewBag.d16 = deger16;
			return View();
		}

		public ActionResult BasitTablolar()
		{
			var sorgu = from x in context.Carilers
						group x by x.CariSehir into g
						select new SinifGrup
						{
							Sehir = g.Key,
							Sayi = g.Count()
						};
			return View(sorgu.ToList());
		}

		public PartialViewResult Partial1()
		{
			var sorgu2 = from x in context.Personels
						 group x by x.Departman.DepartmanAd into g
						 select new SinifGrup2
						 {
							 Departman = g.Key,
							 Sayi = g.Count()
						 };
			return PartialView(sorgu2.ToList());
		}

		public PartialViewResult Partial2()
		{
			var sorgu = context.Carilers.ToList();
			return PartialView(sorgu);
		}

		public PartialViewResult Partial3()
		{
			var sorgu = context.Uruns.ToList();
			return PartialView(sorgu);
		}

		public PartialViewResult Partial4()
		{
			var sorgu = from x in context.Uruns
						group x by x.Marka into g
						select new SinifGrup3
						{
							marka = g.Key,
							sayi = g.Count()
						};
			return PartialView(sorgu.ToList());
		}
	}
}
