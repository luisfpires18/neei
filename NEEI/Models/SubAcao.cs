using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEEI.Models
{
    public class SubAcao
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Desrição")]
        public string descricao { get; set; }

        [Required]
        [Display(Name = "Receita")]
        public float receita { get; set; }

        [Required]
        [Display(Name = "Despesa")]
        public float despesa { get; set; }

        [Required]
        [Display(Name = "Ação")]
        public int AcaoId { get; set; }

        public Acao Acaos { get; set; }
    }
}