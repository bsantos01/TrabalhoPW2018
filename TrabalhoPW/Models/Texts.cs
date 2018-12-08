using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoPW.Models
{
    public class Texts
    {
        [Key]
        public int TextID { get; set; }
        [MaxLength(50)]
        [Display(Name = "Pagina (Titulo)")]
        public string Pagina { get; set; }
        [MaxLength(150)]
        [Display(Name = "Subtitulo")]
        public string SubT { get; set; }

        [MaxLength(200)]
        [Display(Name = "Conteudo")]
        public string Conteudo { get; set; }
    }
}