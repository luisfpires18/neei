using System.Collections.Generic;
using NEEI.Models;

namespace NEEI.ViewModels
{
    public class SubAcaoViewModel
    {
        public SubAcao subA { get; set; }

        public Acao a { get; set; }

        public IEnumerable<Acao> aList { get; set; }
    }
}