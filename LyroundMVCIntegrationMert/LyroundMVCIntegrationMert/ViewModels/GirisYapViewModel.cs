using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.ViewModels
{
    public class GirisYapViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), MinLength(5), MaxLength(50)]
        public string KullaniciAdi { get; set; }
        [DisplayName("Sifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), MinLength(6), MaxLength(50)]
        public string Sifre { get; set; }

    }
}