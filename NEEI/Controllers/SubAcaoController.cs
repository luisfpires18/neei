using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using NEEI.Models;

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


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Ver SubAcções;
        public ActionResult Index(int id)
        {
            var acao = _context.Acao.SingleOrDefault(a => a.id == id);
            ViewBag.IdAcao = id;
            ViewBag.Acao = acao.descricao;
            ViewBag.IdRelatorio = acao.RelatorioId;

            var subAcoes = _context.SubAcao.SqlQuery("SELECT * FROM SubAcaos a WHERE a.AcaoId = @id", new SqlParameter("@id", id)).ToList();
            return View("Index", subAcoes);
        }

        // View de adicionar SubAcção;
        public ActionResult New(int id)
        {
            var acao = _context.Acao.SingleOrDefault(a => a.id == id);
            ViewBag.IdAcao = acao.id;
            ViewBag.Acao = acao.descricao;

            return View("SubAcaoForm");
        }
        

        // Criar SubAcção;
        [HttpPost]
        public ActionResult Create(SubAcao subacao)
        {
            // Atualiza a Acção;
            var acao = _context.Acao.SingleOrDefault(a => a.id == subacao.AcaoId);
            acao.lucro += subacao.receita - subacao.despesa;

            //  Atualiza o Relatorio;
            var rc = _context.Relatorio.SingleOrDefault(r => r.id == acao.RelatorioId);
            var acoes = _context.Acao.SqlQuery("SELECT * FROM Acaos a WHERE a.RelatorioId = @id", new SqlParameter("@id", acao.RelatorioId)).ToList();
            rc.total = 0;
            foreach (var a in acoes)
            {
                rc.total += a.lucro;
            }

            subacao.Acaos = acao;

            _context.SubAcao.Add(subacao);
            _context.SaveChanges();

            return RedirectToAction("Index", "SubAcao" + "/Index/" + subacao.AcaoId);
        }

       
    }
}