using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
	public class CariPanelController : Controller
	{
		// GET: CariPanel
		Context context = new Context();
		[Authorize]
		public ActionResult Index()
		{
			var mail = (string)Session["CariMail"];
			//var degerler = context.Carilers.Where(x => x.CariMail == mail).ToList();
			//ViewBag.m = mail;
			var degerler = context.Mesajlars.Where(x => x.Alici == mail).ToList();
			ViewBag.m = mail;
			var mailid = context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
			ViewBag.mid = mailid;
			var toplamsatis = context.SatisHarakets.Where(x => x.CariID == mailid).Count();
			ViewBag.toplamsatis = toplamsatis;
			var toplamtutar = context.SatisHarakets.Where(x => x.CariID == mailid).Sum(y => (decimal?)y.ToplamTutar) ?? 0;
			ViewBag.toplamtutar = toplamtutar;
			var toplamurunsayisi = context.SatisHarakets.Where(x => x.CariID == mailid).Sum(y => (int?)y.Adet) ?? 0;
			ViewBag.toplamurunsayisi = toplamurunsayisi;
			var adsoyad = context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
			ViewBag.adsoyad = adsoyad;
			return View(degerler);
		}
		[Authorize]
		public ActionResult Siparislerim()
		{
			var mail = (string)Session["CariMail"];
			var id = context.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
			var degerler = context.SatisHarakets.Where(x => x.CariID == id).ToList();
			return View(degerler);
		}
		[Authorize]
		public ActionResult GelenMesajlar()
		{
			var mail = (string)Session["CariMail"];
			var mesajlar = context.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajID).ToList();
			var gelensayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(mesajlar);
		}
		[Authorize]
		public ActionResult GidenMesajlar()
		{
			var mail = (string)Session["CariMail"];
			var mesajlar = context.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(z => z.MesajID).ToList();
			var gelensayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(mesajlar);
		}

		public ActionResult MesajDetay(int id)
		{
			var degerler = context.Mesajlars.Where(x => x.MesajID == id).ToList();
			var mail = (string)Session["CariMail"];
			var gelensayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(degerler);
		}
		[Authorize]
		[HttpGet]
		public ActionResult YeniMesaj()
		{
			var mail = (string)Session["CariMail"];
			var gelensayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View();
		}
		[HttpPost]
		public ActionResult YeniMesaj(Mesajlar m)
		{
			var mail = (string)Session["CariMail"];
			m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
			m.Gonderici = mail;
			context.Mesajlars.Add(m);
			context.SaveChanges();
			return View();
		}
		[Authorize]
		public ActionResult KargoTakip(string p)
		{
			var k = from x in context.KargoDetays select x;
			k = k.Where(y => y.TakipKodu.Contains(p));
			return View(k.ToList());
		}
		[Authorize]
		public ActionResult CariKargoTakip(string id)
		{
			var degerler = context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
			return View(degerler);
		}
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "Login");
		}

		public PartialViewResult Partial1()
		{
			var mail = (string)Session["CariMail"];
			var id = context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
			var caribul = context.Carilers.Find(id);
			return PartialView("Partial1", caribul);
		}

		public PartialViewResult Partial2()
		{
			var veriler = context.Mesajlars.Where(x => x.Gonderici == "admin").OrderByDescending(y => y.Tarih).ToList();
			return PartialView(veriler);
		}

		public ActionResult CariBilgiGuncelle(Cariler cr)
		{
			var cari = context.Carilers.Find(cr.CariID);
			cari.CariAd = cr.CariAd;
			cari.CariSoyad = cr.CariSoyad;
			cari.CariSifre = cr.CariSifre;
			context.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}