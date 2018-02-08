using LyroundMVCIntegrationMert.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.ViewModels
{
    public class SarkiEkleViewModel
    {

        public List<Arkadas> ArkadasList { get; set; }
        public Arkadas Arkadaslar { get; set; }


        public HttpPostedFileBase ImageFile { get; set; }
        public List<Sarki> SarkilarList { get; set; }
        public Uye Uyeler { get; set; }
        public List<HashTag> HashTagList { get; set; }

        public UyeResim UyeResim { get; set; }
        public List<UyeResim> UyeResimler { get; set; }
        public List<Uye> UyelerList { get; set; }

        public Yorum Yorum { get; set; }
        public List<Yorum> YorumList { get; set; }
        public List<PaylasilanText> PaylasilanTextList { get; set; }
        public Sarki Sarkilar { get; set; }
        //[DisplayName("Paylaşılan Text"), Required(), MinLength(2), MaxLength(50)]
        public PaylasilanText PaylasilanTextler { get; set; }
        //[DisplayName("Şarkı Resmi"), DataType(DataType.ImageUrl)]

        public List<SarkiResim> SarkiResimList { get; set; }
        public SarkiResim SarkiResimler { get; set; }
        //[DisplayName("HasTagler"), MinLength(3), MaxLength(25)]
        public HashTag HashTagler { get; set; }
    }
}