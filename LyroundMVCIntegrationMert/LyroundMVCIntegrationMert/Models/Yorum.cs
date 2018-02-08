using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{
    [Table("Yorum")]
    public class Yorum
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YorumId { get; set; }
        [Required]
        public string YorumIcerik { get; set; }
        [Required]
        public DateTime YorumTarihi { get; set; }

        [Required]
        public int SarkiId { get; set; }
        [Required]
        public int UyeId { get; set; }


        public Sarki Sarki { get; set; }

        public Uye Uye { get; set; }
    }
}