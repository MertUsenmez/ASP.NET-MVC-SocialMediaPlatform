using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.ViewModels
{
    public class UyeOlViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(), MinLength(5), MaxLength(50) ]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre"), Required(), MinLength(6), MaxLength(50), DataType(DataType.Password)]
        public string Sifre { get; set; }

        [DisplayName("E-Posta"), Required(), MaxLength(50), EmailAddress()]
        public string EMail { get; set; }
        [DisplayName("Ad"), Required(), MaxLength(50)]
        public string Ad { get; set; }
        [DisplayName("Soyad"), Required(), MaxLength(50)]
        public string Soyad { get; set; }
        
        //public bool Cinsiyet { get; set; }
    }
}