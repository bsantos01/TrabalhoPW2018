using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoPW.Models
{


    public class Utilizador
    {
        public int UtilizadorID { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(150)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "BI")]
        public int BI { get; set; }

        [Required]
        [Display(Name = "NIF")]
        public int NIF { get; set; }

        [Required]
        [Display(Name = "Tipo de Utilizador")]
        public string Tipo { get; set; }

        [Required]
        public bool Valido{ get; set; }


       // [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}