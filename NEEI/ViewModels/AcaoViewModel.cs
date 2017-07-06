using System.Collections.Generic;
using NEEI.Models;

namespace NEEI.ViewModels
{
    public class AcaoViewModel
    {
        public Acao a { get; set; }

        public RelatorioContas rc { get; set; }

        public IEnumerable<RelatorioContas> rcList { get; set; }
    }
}