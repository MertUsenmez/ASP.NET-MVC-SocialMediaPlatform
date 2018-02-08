using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{
    [Table("Sarki")]
    public class Sarki
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SarkiId { get; set; }
        [Required]
        public string Baslik { get; set; }
        [Required]
        public bool TamamlandiMi { get; set; }
        [Required]
        public DateTime OlusturulmaTarihi { get; set; }
        [Required]
        public int UyeId { get; set; }

        public Uye Uye { get; set; }
        public List<Begeni> Begeniler { get; set; }
        public List<HashTag> HashTagler { get; set; }
        public List<PaylasilanText> PaylasilanTextler { get; set; }
        public List<Yorum> Yorumlar { get; set; }
        public List<SarkiResim> SarkiResimleri { get; set; }

    }
}