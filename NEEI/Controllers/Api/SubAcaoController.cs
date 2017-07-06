using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NEEI.Models;

namespace NEEI.Controllers.Api
{
    public class SubAcaoController : ApiController
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


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            // Procura SubAcção por Id;
            var subAccao = _context.SubAcao.SingleOrDefault(a => a.id == id);

            // Procura Acção por Id;
            var acao = _context.Acao.SingleOrDefault(a => a.id == subAccao.AcaoId);
            // Calcula o Lucro;
            var lucro = subAccao.receita - subAccao.despesa;

            // Se for positivo, retira-se esse valor;
            if (lucro > 0)
                acao.lucro = acao.lucro - lucro;

            // Se for negativo, aumenta esse valor;
            if (lucro < 0)
                acao.lucro = acao.lucro + lucro;

            // Atualiza o Total do Relatorio de Contas;
            var rc = _context.Relatorio.SingleOrDefault(r => r.id == acao.RelatorioId);
            var acoes = _context.Acao.SqlQuery("SELECT * FROM Acaos a WHERE a.RelatorioId = @id", new SqlParameter("@id", acao.RelatorioId)).ToList();

            rc.total = 0;
            foreach (var a in acoes)
            {
                rc.total += a.lucro;
            }

            _context.SubAcao.Remove(subAccao);
            _context.SaveChanges();

            return Ok();
        }
    }
}
