using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{
    [Table("Uye")]
    public class Uye
    {
        public Uye()
        {

        }
        public Uye(string KullaniciAdi,string Sifre,string EMail,string Ad,string Soyad,
           DateTime UyeOlmaTarihi,int BasariDurumu)
        {
            this.KullaniciAdi = KullaniciAdi;
            this.Sifre = Sifre;
            this.EMail = EMail;
            this.Ad = Ad;
            this.Soyad = Soyad;
            this.UyeOlmaTarihi = UyeOlmaTarihi;
            this.BasariDurumu = BasariDurumu;

        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UyeId { get; set; }

        [StringLength(50), Required]
        public string KullaniciAdi { get; set; }

        [StringLength(50), Required]     /*PasswordPropertyText*/
        public string Sifre { get; set; }

        [StringLength(50),  Required]
        public string EMail { get; set; }

        [StringLength(50), Required]
        public string Ad { get; set; }

        [StringLength(50), Required]
        public string Soyad { get; set; }
        [Required]
        public int BasariDurumu { get; set; }
        [Required]
        public DateTime UyeOlmaTarihi { get; set; }


        [InverseProperty("Uye")]
        public ICollection<Arkadas> Uyeler { get; set; }

        [InverseProperty("UyeArkadasi")]
        public ICollection<Arkadas> UyeArkadaslari { get; set; }

        [InverseProperty("GonderenUye")]
        public ICollection<Mesaj> GonderilenMesajlar { get; set; }

        [InverseProperty("AlanUye")]
        public ICollection<Mesaj> AlinanMesajlar { get; set; }


        public List<Sarki> Sarkilar { get; set; }
        public List<Begeni> Begeniler { get; set; }
        public List<Yorum> Yorumlar { get; set; }
        public List<PaylasilanText> PaylasilanTextler { get; set; }
        public List<UyeResim> UyeResimleri { get; set; }
    }
    
}