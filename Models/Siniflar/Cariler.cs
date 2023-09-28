using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Siniflar
{
	public class Cariler
	{
		[Key]
		public int CariID { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(30)]
		[Required(ErrorMessage = "Bu alanı boş geçemezsiniz!")]
		public string CariAd { get; set; }


		[Column(TypeName = "VarChar")]
		[StringLength(30)]
		[Required(ErrorMessage = "Bu alanı boş geçemezsiniz!")]
		public string CariSoyad { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(20)]
		[Required(ErrorMessage = "Bu alanı boş geçemezsiniz!")]
		public string CariSehir { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(50)]
		public string CariMail { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(20)]
		public string CariSifre { get; set; }
		public bool Durum { get; set; }

		public ICollection<SatisHareket> SatisHarekets { get; set; }

	}
}