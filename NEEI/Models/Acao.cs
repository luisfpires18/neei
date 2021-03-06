﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEEI.Models
{
    public class Acao
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Total")]
        public float Total { get; set; }

        [Required]
        [Display(Name = "Relatório de Contas")]
        public int RelatorioId { get; set; }

        public Relatorio Relatorios { get; set; }
    }
}