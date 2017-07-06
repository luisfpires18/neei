using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEEI.Models
{
    public class Relatorio
    {

        public int id { get; set; }

        [Required]
        [Display(Name = "Relatório")]
        public string descricao { get; set; }

        [Display(Name = "Total")]
        public float total { get; set; }
    }
}