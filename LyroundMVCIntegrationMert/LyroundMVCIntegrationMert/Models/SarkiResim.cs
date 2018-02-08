using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{
    [Table("SarkiResim")]
    public class SarkiResim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SarkiResimId { get; set; }
        [Required,StringLength(150)]
        public string SarkiResimYolu { get; set; }
        [Required]
        public int SarkiId { get; set; }


        public Sarki Sarki { get; set; }
    }
}