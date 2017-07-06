using System.Linq;
using System.Web.Mvc;
using NEEI.Models;
using NEEI.ViewModels;

namespace NEEI.Controllers
{
    public class AcaoController : Controller
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public AcaoController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Ver Acções;
        public ActionResult Index()
        {
           // var rc = _context.RelatorioContas.SingleOrDefault(r => r.id == id);



            var acoes = _context.Acao.ToList();


            return View(acoes);
        }

        // View de adicionar Acção;
        public ActionResult New()
        {
            var viewModel = new AcaoViewModel
            {
                rcList = _context.RelatorioContas.ToList(),
            };
            return View("AcaoForm", viewModel);
        }

        // Criar Acção;
        [HttpPost]
        public ActionResult Create(Acao a)
        {
            _context.Acao.Add(a);
            _context.SaveChanges();

            return RedirectToAction("Index", "Acao");
        }
    }
}