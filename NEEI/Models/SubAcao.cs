using System.ComponentModel.DataAnnotations;

namespace NEEI.Models
{
    public class SubAcao
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public float receita { get; set; }
        public float despesa { get; set; }

        [Required]
        public int AcaoId { get; set; }

        public Acao acao { get; set; }
    }
}