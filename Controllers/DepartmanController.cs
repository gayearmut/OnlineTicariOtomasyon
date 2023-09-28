using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
	[Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context context = new Context();
        public ActionResult Index()
        {
			var departmanlar = context.Departmans.Where(x => x.Durum == true).ToList();
			return View(departmanlar);
        }

		[HttpGet]
		public ActionResult DepartmanEkle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult DepartmanEkle(Departman d)
		{
			context.Departmans.Add(d);
			d.Durum = true;
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult DepartmanSil(int id)
		{
			var dprtmn = context.Departmans.Find(id);
			dprtmn.Durum = false;
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult DepartmanGetir(int id)
		{
			var dprtmn = context.Departmans.Find(id);
			return View("DepartmanGetir", dprtmn);
		}

		public ActionResult DepartmanGuncelle(Departman p)
		{
			var dprtmn = context.Departmans.Find(p.DepartmanID);

			dprtmn.DepartmanAd = p.DepartmanAd;

			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult DepartmanDetay(int id)
		{
			var personeller = context.Personels.Where(x => x.DepartmanID == id).ToList();//hangi departmanda kimler çalışır sorgusu
			var dprtmn = context.Departmans.Where(x => x.DepartmanID == id).Select(y => y.DepartmanAd).FirstOrDefault();//departman adını çekme sorgusu
			ViewBag.departman = dprtmn;
			return View(personeller);
		}
		public ActionResult DepartmanPersonelSatis(int id)
		{
			var satislar = context.SatisHarakets.Where(x => x.PersonelID == id).ToList();
			var prsnl = context.Personels.Where(x => x.PersonelID == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
			ViewBag.personel = prsnl;
			return View(satislar);
		}



	}
}