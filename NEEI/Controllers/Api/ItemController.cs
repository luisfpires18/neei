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
    public class ItemController : ApiController
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public ItemController()
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
            // Procura Tudo;
            var item = _context.Item.SingleOrDefault(a => a.Id == id);
            var categoria = _context.Categoria.SingleOrDefault(c => c.Id == item.CategoriaId);
            var acao = _context.Acao.SingleOrDefault(a => a.Id == categoria.AcaoId);
            var relatorio = _context.Relatorio.SingleOrDefault(r => r.Id == acao.RelatorioId);

            // Calcula o Lucro;
            var lucro = item.Receita - item.Despesa;

            // Se for positivo, retira-se esse valor;
            if (lucro > 0)
            {
                categoria.Lucro = categoria.Lucro - lucro;
                acao.Total = acao.Total - lucro;
                relatorio.Valor = relatorio.Valor - lucro;
            }

            // Se for negativo, aumenta esse valor;
            if (lucro < 0)
            {
                categoria.Lucro = categoria.Lucro + lucro * (-1);
                acao.Total = acao.Total + lucro * (-1);
                relatorio.Valor = relatorio.Valor + lucro * (-1);
            }
     
            // Atualiza dados;
            _context.Item.Remove(item);
            _context.SaveChanges();
            return Ok();
        }
    }
}
