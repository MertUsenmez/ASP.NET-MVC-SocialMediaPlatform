using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LyroundMVCIntegrationMert.Models
{
    [Table("UyeResim")]
    public class UyeResim
    {
        public UyeResim()
        {
            UyeResimYolu = "/UploadImages/avatar.png";
        }


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UyeResimId { get; set; }

        [Required]
        public string UyeResimYolu { get; set; }

        //Foreign Key Standart Kullanımıdır.
        public int UyeId { get; set; }

        public Uye Uye { get; set; }


    }
}