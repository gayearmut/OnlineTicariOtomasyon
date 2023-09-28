using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Siniflar
{
	public class Urun
	{

		[Key]
		public int Urunid { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(30)]
		public string UrunAd { get; set; }


		[Column(TypeName = "VarChar")]
		[StringLength(30)]
		public string Marka { get; set; }


		public short Stok { get; set; }
		public int AlisFiyat { get; set; }
		public string SatisFiyat { get; set; }
		public bool Durum { get; set; }

		[Column(TypeName = "VarChar")]
		[StringLength(250)]
		public string UrunGorsel { get; set; }
		public int KategoriID { get; set; }
		public virtual Kategori Kategori { get; set; }

		public ICollection<SatisHareket> SatisHarekets { get; set; }

	}
}