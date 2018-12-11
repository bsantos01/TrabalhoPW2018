using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoPW.Models
{
    public class Tratamento
    {
        [Key]
        public int TratID { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Descrição de Tratamento")]
        public string Desc { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Tratamento")]
        public DateTime Data { get; set; }

        [ForeignKey("Objeto")]
        public int ObjID { get; set; }
        public Objeto Objeto { get; set; }
    }
}