using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{

    [Table("Arkadas")]
    public class Arkadas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArkadasId { get; set; }

        [Required]
        public DateTime ArkadaslikTarihi { get; set; }

        [Required]
        public int UyeId { get; set; }
        [Required]
        public int UyeArkadasId { get; set; }


        [ForeignKey("UyeId")]
        public Uye Uye { get; set; }
        [ForeignKey("UyeArkadasId")]
        public Uye UyeArkadasi { get; set; }
        

    }
}