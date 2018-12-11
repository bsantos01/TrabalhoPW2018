using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoPW.Models
{
    public enum Estado { ND, Excelente , Bom , Razoavel, Mau };

    public class Aluguer
    {

        public int AluguerID{get; set;}

        [ForeignKey("Objeto")]
        public int ObjID { get; set; }
        public Objeto Objeto { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Incial do Aluguer")]
        public DateTime? DataIncio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Final do Aluguer")]
        public DateTime? DataFim { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Entrega")]
        public DateTime? DataEntrega { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Finalidade Aluguer")]
        public string Finalidade { get; set; }

        [Display(Name = "Validação")]
        public bool Validado { get; set; }

        [ForeignKey("Requerente")]
        [Display(Name = "Requerente do Aluguer")]
        public int RequerenteID { get; set; }
        public Utilizador Requerente { get; set; }

        [Display(Name = "Estado Inicial")]
        public Estado EstadoI { get; set; }

        [Display(Name = "Estado Final")]
        public Estado EstadoF { get; set; }

        [MaxLength(200)]
        [Display(Name = "Relatório Final")]
        public string Relatorio { get; set; }

    }
}