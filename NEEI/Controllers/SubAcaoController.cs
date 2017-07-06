using System.Linq;
using System.Web.Mvc;
using NEEI.Models;
using NEEI.ViewModels;

namespace NEEI.Controllers
{
    public class SubAcaoController : Controller
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public SubAcaoController()
        {
            _context = new ApplicationDbContext();
        }

        public void getTotal(int id)
        {
            var rc = _context.RelatorioContas.SingleOrDefault(r => r.id == id);
            var acoes = _context.Acao.ToList();

            rc.total = 0;
            foreach (var a in acoes)
            {
                rc.total += a.lucro;
            }
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Ver SubAcções;
        public ActionResult Index()
        {
            var subAcoes = _context.SubAcao.ToList();
            return View(subAcoes);
        }

        // View de adicionar sub-acções;
        public ActionResult New()
        {
            var viewModel = new SubAcaoViewModel
            {
                aList = _context.Acao.ToList(),
            };
            return View("SubAcaoForm", viewModel);
        }

        // Criar SubAcção;
        [HttpPost]
        public ActionResult Create(SubAcao subA)
        {
            var acao =_context.Acao.SingleOrDefault(a => a.id == subA.AcaoId);
            acao.lucro += subA.receita - subA.despesa;

            getTotal(acao.RelatorioId);

            _context.SubAcao.Add(subA);
            _context.SaveChanges();

            return RedirectToAction("Index", "SubAcao");
        }

        [HttpDelete]
        public ActionResult Delete(SubAcao s)
        {
            var subAccao = _context.SubAcao.SingleOrDefault(a => a.id == s.id);

            var acao = _context.Acao.SingleOrDefault(a => a.id == subAccao.AcaoId);
            var lucro = subAccao.receita - subAccao.despesa;

            if (lucro > 0)
                acao.lucro = acao.lucro - lucro;

            if(lucro < 0)
                acao.lucro = acao.lucro + lucro;

            getTotal(acao.RelatorioId);

            _context.SubAcao.Remove(subAccao);
            _context.SaveChanges();

            return RedirectToAction("Index", "SubAcao");

        }
    }
}