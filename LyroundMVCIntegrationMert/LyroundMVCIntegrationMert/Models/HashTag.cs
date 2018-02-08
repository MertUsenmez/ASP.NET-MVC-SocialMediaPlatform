using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{
    [Table("HashTag")]
    public class HashTag
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HashTagId { get; set; }
        [Required]
        public string HashTagIcerik { get; set; }

        public int SarkiId { get; set; }

        public Sarki Sarki { get; set; }
    }
}