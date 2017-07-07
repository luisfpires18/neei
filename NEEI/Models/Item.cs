using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEEI.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Titular")]
        public string Titular { get; set; }

        [Display(Name = "Receita")]
        public float Receita { get; set; }

        [Display(Name = "Despesa")]
        public float Despesa { get; set; }

        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public Categoria Categorias { get; set; }
    }
}