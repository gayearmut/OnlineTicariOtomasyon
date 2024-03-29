﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Siniflar
{
	public class Fatura
	{
		[Key]
		public int FaturaID { get; set; }

		[Column(TypeName = "Char")]
		[StringLength(1)]
		public string FaturaSeriNo { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(6)]
		public string FaturaSiraNo { get; set; }
		public DateTime Tarih { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(60)]
		public string VergiDairesi { get; set; }

		
		public string Saat { get; set; }
		public decimal Toplam { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(30)]
		public string TeslimAlan { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(30)]
		public string TeslimEden { get; set; }

		public ICollection<FaturaKalem> FaturaKalems { get; set; }

	}
}