﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace Online_Ticari_Otomasyon.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		// GET: Login
		Context context = new Context();
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public PartialViewResult Partial1()
		{
			return PartialView();
		}
		[HttpPost]
		public PartialViewResult Partial1(Cariler p)
		{
			context.Carilers.Add(p);
			p.Durum = true;
			context.SaveChanges();
			return PartialView();
		}
		[HttpGet]
		public ActionResult CariLogin1()
		{
			return View();
		}
		[HttpPost]
		public ActionResult CariLogin1(Cariler p)
		{
			var bilgiler = context.Carilers.FirstOrDefault(x => x.CariMail == p.CariMail && x.CariSifre == p.CariSifre);
			if (bilgiler != null)
			{
				FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
				Session["CariMail"] = bilgiler.CariMail.ToString();
				return RedirectToAction("Index", "CariPanel");
			}
			else
			{
				return RedirectToAction("Index", "Login");
			}
		}
		[HttpGet]
		public ActionResult AdminLogin()
		{
			return View();
		}
		public ActionResult AdminLogin(Admin p)
		{
			var bilgiler = context.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);
			if (bilgiler != null)
			{
				FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);
				Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString();
				return RedirectToAction("Index", "Kategori");
			}
			else
			{
				return RedirectToAction("Index", "Login");
			}
		}
	}
}