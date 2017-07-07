using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEEI.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Lucro")]
        public float Lucro { get; set; }

        [Required]
        [Display(Name = "Ação")]
        public int AcaoId { get; set; }

        public Acao Acaos { get; set; }
    }
}