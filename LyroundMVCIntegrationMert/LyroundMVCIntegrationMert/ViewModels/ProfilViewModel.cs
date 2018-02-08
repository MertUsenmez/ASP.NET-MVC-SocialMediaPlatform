using LyroundMVCIntegrationMert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.ViewModels
{
    public class ProfilViewModel
    {
        public HttpPostedFileBase ProfilImageFile { get; set; }

        public Uye Uyeler { get; set; }
        public List<Arkadas> Arkadaslar { get; set; }

        public UyeResim UyeResim { get; set; }
        public List<PaylasilanText> PaylasilanTextler { get; set; }
    }
}