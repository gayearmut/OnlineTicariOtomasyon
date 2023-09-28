using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
    public class PersonelController : Controller
    {
		// GET: Personel
		Context context = new Context();
		public ActionResult Index()
        {
			var personeller = context.Personels.Where(x => x.Durum == true).ToList();
			return View(personeller);
        }

		[HttpGet]
		public ActionResult PersonelEkle()
		{
			List<SelectListItem> departmanlar = (from x in context.Departmans.ToList()
												 select new SelectListItem
												 {
													 Text = x.DepartmanAd,
													 Value = x.DepartmanID.ToString()
												 }).ToList();
			ViewBag.dprtmn = departmanlar;

			return View();
		}
		[HttpPost]
		public ActionResult PersonelEkle(Personel p)
		{
			if (Request.Files.Count > 0)
			{
				string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
				string uzanti = Path.GetExtension(Request.Files[0].FileName);
				string yol = "~/Image/" + dosyaadi + uzanti;
				Request.Files[0].SaveAs(Server.MapPath(yol));
				p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
			}
			p.Durum = true;
			context.Personels.Add(p);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult PersonelGetir(int id)
		{
			List<SelectListItem> departmanlar = (from x in context.Departmans.ToList()
												 select new SelectListItem
												 {
													 Text = x.DepartmanAd,
													 Value = x.DepartmanID.ToString()
												 }).ToList();
			ViewBag.dprtmn = departmanlar;

			var personel = context.Personels.Find(id);
			return View("PersonelGetir", personel);


		}
		public ActionResult PersonelGuncelle(Personel p)
		{
			if (Request.Files.Count > 0)
			{
				string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
				string uzanti = Path.GetExtension(Request.Files[0].FileName);
				string yol = "~/Image/" + dosyaadi + uzanti;
				Request.Files[0].SaveAs(Server.MapPath(yol));
				p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
			}
			var personel = context.Personels.Find(p.PersonelID);

			personel.PersonelAd = p.PersonelAd;
			personel.PersonelSoyad = p.PersonelSoyad;
			personel.PersonelGorsel = p.PersonelGorsel;
			personel.DepartmanID = p.DepartmanID;
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult PersonelListe()
		{
			var sorgu = context.Personels.ToList();
			return View(sorgu);
		}

	}
}