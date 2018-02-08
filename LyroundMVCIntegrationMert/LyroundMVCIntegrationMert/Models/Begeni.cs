using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{
    [Table("Begeni")]
    public class Begeni
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BegeniId { get; set; }
        [Required]
        public DateTime BegeniTarihi { get; set; }
        [Required]
        public int SarkiId { get; set; }
        [Required]
        public int UyeId { get; set; }

        public Uye Uye { get; set; }
        public Sarki Sarki { get; set; }

    }
}