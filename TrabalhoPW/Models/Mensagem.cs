using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoPW.Models
{
    public class Mensagem
    {

        public int MensagemID { get; set; }


        [MaxLength(150)]
        [Display(Name = "Remetente da Mensagem")]
        public string RemNome{ get; set; }
       

        [Required]
        [ForeignKey("Destinatario")]
        [Display(Name = "Destinatario da Mensagem")]
        public int DestinatarioID { get; set; }
        public Utilizador Destinatario { get; set; }

        [Required]
        [Display(Name = "Mensagem")]
        [MaxLength(200)]
        public string Conteudo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da Mensagem")]
        public DateTime? data { get; set; }
    }
}