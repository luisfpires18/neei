using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEEI.Models
{
    public class Acao
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Display(Name = "Lucro")]
        public float lucro { get; set; }

        [Required]
        [Display(Name = "Relatório de Contas")]
        public int RelatorioId { get; set; }

        public Relatorio Relatorios { get; set; }
    }
}