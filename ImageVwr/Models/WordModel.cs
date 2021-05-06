using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ImageVwr.Models
{
    public class WordModel
    {
        [Key]
        public long WordId { get; set; }

        [DisplayName("URL")]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Word Name")]
        public string WordName { get; set; }

        [NotMapped]
        [DisplayName("Occurrences")]
        public int Occurrences { get; set; }
    }
}
