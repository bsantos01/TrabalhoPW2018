using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoPW.Models
{
    public enum OrigemP {ND=0, Doação=1, Escavação=2 }
    public enum PeriodoP { ND=0, Préhistória=1, IdadeAntiga=2, IdadeMédia=3, Moderna=4, Contemporanea=5 }

    public class Objeto
    {
        [Key]
        public int ObjID { get; set;}

        [Required]
        [MaxLength(50)]
        [Display(Name = "Tipo de Peça")]
        public string Tipo { get; set; }

        [Required]
        [Display(Name = "Periodo temporal")]
        public PeriodoP Periodo { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Zona de origem")]
        public string Zona { get; set; }

        [Required]
        [Display(Name = "Proveniencia da peça")]
        public OrigemP Origem { get; set; }

        [Display(Name = "Descrição da peça")]
        [MaxLength(200)]
        public string Descricao { get; set; }

        IList<Tratamento> Tratamentos { get; set; }

    }
}