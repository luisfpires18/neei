using System.ComponentModel.DataAnnotations;

namespace NEEI.Models
{
    public class Acao
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public float lucro { get; set; }

        [Required]
        public int RelatorioId { get; set; }

        public RelatorioContas relatorioContas { get; set; }
    }
}