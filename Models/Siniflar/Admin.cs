using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Siniflar
{
	public class Admin
	{
		[Key]
		public int AdminID { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(20)]
		public string KullaniciAd { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(30)]
		public string Sifre { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(30)]
		public string Yetki { get; set; }
	}
}