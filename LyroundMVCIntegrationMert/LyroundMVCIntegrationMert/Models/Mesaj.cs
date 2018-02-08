using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{
    [Table("Mesaj")]
    public class Mesaj
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MesajId { get; set; }
        public string MesajIcerigi { get; set; }
        [Required]
        public DateTime MesajTarihi { get; set; }
        [Required]
        public int GonderenId { get; set; }
        [Required]
        public int AlanId { get; set; }


        [ForeignKey("GonderenId")]
        public Uye GonderenUye { get; set; }

        [ForeignKey("AlanId")]
        public Uye AlanUye { get; set; }

    }
}